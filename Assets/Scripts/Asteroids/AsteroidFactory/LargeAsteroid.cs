

namespace Asteroids
{
    public class LargeAsteroid : AsteroidBase
    {
        public override AsteroidType MyObstacleType { get => AsteroidType.LargeAsteroid; }
        public override AsteroidData MyData { get => base.GetData(ResourcesPath.LargeAsteroidDataPath); }

    }
}