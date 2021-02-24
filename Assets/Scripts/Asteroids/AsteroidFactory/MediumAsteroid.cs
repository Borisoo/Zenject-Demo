using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class MediumAsteroid : AsteroidBase
    {
        public override AsteroidType MyObstacleType{ get=> AsteroidType.mediumAsteroid; }
        public override AsteroidData MyData{ get => base.GetData(ResourcesPath.MediumAsteroidDataPath); }

    }
}