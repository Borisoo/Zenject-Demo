using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace Asteroids{
public abstract class AsteroidBase 
{
    public virtual AsteroidType MyObstacleType{get;}
    public virtual AsteroidData MyData{ get; }
    public virtual AsteroidData GetData(string ResourcesPath)
    {
      AsteroidData data= Resources.Load(ResourcesPath) as AsteroidData;
      return data;
    }
}
}