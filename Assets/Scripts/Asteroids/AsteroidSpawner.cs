using UnityEngine;
using Zenject;
using ModestTree;


namespace Asteroids
{
    public sealed class AsteroidSpawner : ITickable, ISpawnerInterface<AsteroidType>
    {
        private LevelHelper m_level;
        private IAsteroidFactoryInterface<AsteroidType, AsteroidData> m_asteroidDataFactoryInterface;

        [Inject]
        public void Construct(IAsteroidFactoryInterface<AsteroidType, AsteroidData> asteroidDataFactoryInterface, LevelHelper levelHelper)
        {
            this.m_asteroidDataFactoryInterface = asteroidDataFactoryInterface;
            this.m_level = levelHelper;
        }

        [Inject] private Asteroid.Factory m_asteroidFactory;
        [Inject] private UFO.Factory m_ufoFactory;
        [Inject] private AsteroidSpawnerSettings m_asteroidSpawnerSettings;
        private float m_timer;
        private bool m_canSpawn;

        public void Tick()
        {
            SpawnAsteroidAtInterval();
        }

        private void SpawnAsteroidAtInterval()
        {
            if (!m_canSpawn) { return; }

            m_timer += Time.deltaTime;
            if (m_timer > m_asteroidSpawnerSettings.spawnDelay)
            {
                m_timer = 0;
                SpawnAsteroid();
            }
        }
        public void Start()
        {
            m_canSpawn = true;
        }

        private void SpawnAsteroid()
        {
            var random_AsteroidType = RandomEnum<AsteroidType>.Get();
            var data = m_asteroidDataFactoryInterface.GetData(random_AsteroidType);

            if (data == null || random_AsteroidType == AsteroidType.none) { return; }

            Asteroid asteroid = m_asteroidFactory.Create();

            InitializeAsteroid(ref asteroid, data, random_AsteroidType);
            asteroid.transform.position = GetRandomStartPosition(asteroid.ScaleFactor);
        }


        public void SpawnAtPosition(AsteroidType type, Vector3 position)
        {
            Asteroid asteroid = m_asteroidFactory.Create();

            asteroid.transform.position = position;

            var data = m_asteroidDataFactoryInterface.GetData(type);
            InitializeAsteroid(ref asteroid, data, type);
        }


        public void InitializeAsteroid(ref Asteroid asteroid, AsteroidData data, AsteroidType type)
        {
            asteroid.transform.localScale = data.scale;
            asteroid.NumberOfFragments = data.numberOfFragments;
            asteroid.Score = data.scorePoints;
            asteroid.AsteroidType = type;
            asteroid.FragmentType = data.fragmentType;
        }

        private Vector3 GetRandomStartPosition(float scale)
        {
            var area = RandomEnum<AreaType>.Get();
            var rand = UnityEngine.Random.Range(0.0f, 1.0f);

            switch (area)
            {
                case AreaType.top:
                    {
                        return new Vector3(m_level.Left + rand * m_level.Width, m_level.Top + scale, 0);
                    }
                case AreaType.bottom:
                    {
                        return new Vector3(m_level.Left + rand * m_level.Width, m_level.Bottom - scale, 0);
                    }
                case AreaType.right:
                    {
                        return new Vector3(m_level.Right + scale, m_level.Bottom + rand * m_level.Height, 0);
                    }
                case AreaType.left:
                    {
                        return new Vector3(m_level.Left - scale, m_level.Bottom + rand * m_level.Height, 0);
                    }
            }

            throw Assert.CreateException();
        }

        public void Stop()
        {
            m_canSpawn = false;
        }
    }

}