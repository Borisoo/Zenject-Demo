using UnityEngine;
using Zenject;
using ModestTree;

namespace Asteroids
{
    public sealed class UFOSpawner : ITickable, ISpawnerInterface<UFOType>
    {
        private UFO.Factory m_ufoFactory;
        private LevelHelper m_level;
        private IUFOFactoryInterface<UFOType, UFOData> m_ufoDataFactory;
        private float m_timer;

        [Inject]
        private UFOSpawnerSettings _settings;
        private bool m_canSpawn;

        [Inject]
        public void Construct(UFO.Factory ufoFactory, LevelHelper level, IUFOFactoryInterface<UFOType, UFOData> ufoDataFactoryInterface)
        {
            m_ufoFactory = ufoFactory;
            m_level = level;
            m_ufoDataFactory = ufoDataFactoryInterface;
        }
        public void Start()
        {
            m_canSpawn = true;
        }

        public void Tick()
        {
            SpawnUFOAtInterval();
        }

        private void SpawnUFOAtInterval()
        {
            if (!m_canSpawn) { return; }

            m_timer += Time.deltaTime;
            if (m_timer > _settings.spawnDelay)
            {
                m_timer = 0;
                SpawnUFO();
            }
        }

        private void SpawnUFO()
        {
            UFO ufo = m_ufoFactory.Create();

            var ufoType = RandomEnum<UFOType>.Get();
            var score = m_ufoDataFactory.GetScore(ufoType);
            var ufoData = m_ufoDataFactory.GetData(ufoType);

            ufo.Score = score;
            ufo.transform.position = GetRandomStartPosition(ufo.Scale);
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

        public void SpawnAtPosition(UFOType type, Vector3 pos)
        {

        }

        public void Stop()
        {
            m_canSpawn = false;
        }
    }
}
