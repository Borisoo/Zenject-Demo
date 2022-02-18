using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class Explosion : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] private float m_lifeTime;
        [SerializeField] private ParticleSystem m_particleSystem;
        private float m_startTime;
        private IMemoryPool m_pool;

        public void Update()
        {
            if (Time.realtimeSinceStartup - m_startTime > m_lifeTime)
            {
                m_pool.Despawn(this);
            }
        }

        public void OnDespawned() { }

        public void OnSpawned(IMemoryPool pool)
        {
            m_particleSystem.Clear();
            m_particleSystem.Play();

            m_startTime = Time.realtimeSinceStartup;
            m_pool = pool;
        }

        public class Factory : PlaceholderFactory<Explosion>
        {
        }
    }
}

