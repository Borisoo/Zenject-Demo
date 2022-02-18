using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;

namespace Asteroids
{
    public class AsteroidDataFactory : IAsteroidFactoryInterface<AsteroidType, AsteroidData>
    {
        private static Dictionary<AsteroidType, Type> m_asteroidsByTag;
        private static bool m_isInitialized => m_asteroidsByTag != null;
        private static void InitializeFactory()
        {
            if (m_isInitialized)
                return;

            var sizes = Assembly.GetAssembly(typeof(AsteroidBase)).GetTypes()
                .Where(attribute => attribute.IsClass
                && !attribute.IsAbstract
                && attribute.IsSubclassOf(typeof(AsteroidBase)));

            m_asteroidsByTag = new Dictionary<AsteroidType, Type>();

            foreach (var type in sizes)
            {
                var tempAttribute = Activator.CreateInstance(type) as AsteroidBase;
                m_asteroidsByTag.Add(tempAttribute.MyObstacleType, type);
            }
        }

        public AsteroidData GetData(AsteroidType obstacleType)
        {
            InitializeFactory();
            if (m_asteroidsByTag.ContainsKey(obstacleType))
            {
                Type type = m_asteroidsByTag[obstacleType];

                var asteroidItem = Activator.CreateInstance(type) as AsteroidBase;
                var data = asteroidItem.MyData;
                return data;
            }

            return null;
        }

        internal static IEnumerable<AsteroidType> GetSizesByType()
        {
            return m_asteroidsByTag.Keys;
        }
    }

    public interface IAsteroidFactoryInterface<T, K>
    {
        K GetData(T t);
    }

}
