using UnityEngine;
using Zenject;
using ModestTree;

namespace Asteroids
{
    public sealed class Bullet : MonoBehaviour, IPoolable<BulletType, IMemoryPool>, IProjectileInterface
    {
        private float m_startTime;
        private IMemoryPool m_pool;
        private BulletType m_bulletType;

        [SerializeField] private Material _playerMaterial = null;
        [SerializeField] private Material _enemyMaterial = null;

        [Inject]
        private BulletSettings m_bulletSettings;
        private MeshRenderer m_renderer;

        public BulletType Type
        {
            get { return m_bulletType; }
        }

        private void Update()
        {
            if (Time.realtimeSinceStartup - m_startTime > m_bulletSettings.LifeTime)
            {
                m_pool.Despawn(this);
            }
        }

        public void OnSpawned(BulletType type, IMemoryPool pool)
        {
            m_pool = pool;
            m_bulletType = type;
            m_renderer = GetComponent<MeshRenderer>();
            m_renderer.material = type == BulletType.FromEnemy ? _enemyMaterial : _playerMaterial;
            m_startTime = Time.realtimeSinceStartup;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out SpaceObjectBehaviour<BulletType, IProjectileInterface> ship))
            {
                ship.Kill(m_bulletType, this);
            }
        }

        public void DestroyProjectile()
        {
            gameObject.SetActive(false);
        }

        public void OnDespawned()
        {
            m_pool = null;
        }

        private void OnDisable()
        {
            if (m_pool != null)
                m_pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<BulletType, Bullet> { }
    }
}