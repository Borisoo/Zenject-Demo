using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class LargeAsteroid : AsteroidBase
    {
        public override AsteroidType MyObstacleType { get => AsteroidType.LargeAsteroid; }
        public override AsteroidData MyData { get => base.GetData(ResourcesPath.LargeAsteroidDataPath); }

    }
}