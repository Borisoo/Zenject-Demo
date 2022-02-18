
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;

namespace Asteroids
{
    public class UFODataFactory : IUFOFactoryInterface<UFOType, UFOData>
    {
        private static Dictionary<UFOType, Type> UFOByTag;
        private static bool IsInitialized => UFOByTag != null;
        private static void InitializeFactory()
        {
            if (IsInitialized)
                return;

            var ufoTypes = Assembly.GetAssembly(typeof(UFOBase)).GetTypes()
                .Where(attribute => attribute.IsClass && !attribute.IsAbstract && attribute.IsSubclassOf(typeof(UFOBase)));

            UFOByTag = new Dictionary<UFOType, Type>();

            foreach (var type in ufoTypes)
            {
                var tempAttribute = Activator.CreateInstance(type) as UFOBase;
                UFOByTag.Add(tempAttribute.MyType, type);
            }
        }

        public int GetScore(UFOType ufoType)
        {
            InitializeFactory();

            if (UFOByTag.ContainsKey(ufoType))
            {
                Type type = UFOByTag[ufoType];

                var ufoItem = Activator.CreateInstance(type) as UFOBase;
                var points = ufoItem.Points;
                return points;
            }
            return 0;
        }

        public UFOData GetData(UFOType ufoType)
        {
            InitializeFactory();

            if (UFOByTag.ContainsKey(ufoType))
            {
                Type type = UFOByTag[ufoType];

                var ufoItem = Activator.CreateInstance(type) as UFOBase;
                var ufoData = ufoItem.MyData;
                return ufoData;
            }
            return null;
        }
    }

    public interface IUFOFactoryInterface<T, K>
    {
        int GetScore(T t);
        K GetData(T t);
    }
}
