using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRequireDependency<T>
{
   T DependencyInterface{get;set;}
}
