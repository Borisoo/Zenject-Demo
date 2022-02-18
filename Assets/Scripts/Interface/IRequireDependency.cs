using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
      public interface IRequireDependency<T>
      {
         T DependencyInterface { get; set; }
      }
}