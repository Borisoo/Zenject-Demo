
namespace Asteroids
{
    public class SmallAsteroid : AsteroidBase
    {
        public override AsteroidType MyObstacleType { get => AsteroidType.smallAsteroid; }
        public override AsteroidData MyData { get => base.GetData(ResourcesPath.SmallAsteroidDataPath); }

    }
}