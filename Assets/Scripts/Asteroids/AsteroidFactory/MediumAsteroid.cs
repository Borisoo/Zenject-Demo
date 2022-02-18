
namespace Asteroids
{
    public class MediumAsteroid : AsteroidBase
    {
        public override AsteroidType MyObstacleType { get => AsteroidType.mediumAsteroid; }
        public override AsteroidData MyData { get => base.GetData(ResourcesPath.MediumAsteroidDataPath); }

    }
}