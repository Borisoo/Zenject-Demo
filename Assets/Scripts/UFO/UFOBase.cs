using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Asteroids
{
   public abstract class UFOBase
   {
      public virtual UFOType MyType{get; } 
      public virtual int Points{ get; }
      public virtual UFOData MyData{ get; }
      public UFOData GetData(string ResourcesPath)
      {
         UFOData data= Resources.Load(ResourcesPath) as UFOData;
         return data;
      }
   }
}