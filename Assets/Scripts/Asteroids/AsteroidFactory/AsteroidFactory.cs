using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

namespace Asteroids
{
public class AsteroidDataFactory : IAsteroidFactoryInterface<AsteroidType,AsteroidData>
{
    private static  Dictionary<AsteroidType,Type> AsteroidsByTag;
    private static bool IsInitialized => AsteroidsByTag != null;
    private static  void InitializeFactory()
    {
       if(IsInitialized)
        return;

        var sizes = Assembly.GetAssembly(typeof(AsteroidBase)).GetTypes()
            .Where(attribute => attribute.IsClass 
            && !attribute.IsAbstract 
            && attribute.IsSubclassOf(typeof(AsteroidBase)));

        AsteroidsByTag = new Dictionary<AsteroidType, Type>();

        foreach(var type in sizes)
        {
            var tempAttribute = Activator.CreateInstance(type) as AsteroidBase;
            AsteroidsByTag.Add(tempAttribute.MyObstacleType, type);
        }
    }

    public AsteroidData GetData(AsteroidType obstacleType)
    {
        InitializeFactory();
        if(AsteroidsByTag.ContainsKey(obstacleType))
        {
            Type type = AsteroidsByTag[obstacleType];

            var asteroidItem = Activator.CreateInstance(type) as AsteroidBase; 
            var data = asteroidItem.MyData;
            return data;
        }
        
        return null;
    }

    internal static IEnumerable<AsteroidType> GetSizesByType()
    {
        return AsteroidsByTag.Keys;
    }
}

public interface IAsteroidFactoryInterface<T,K>
{
    K GetData(T t);
}

}
