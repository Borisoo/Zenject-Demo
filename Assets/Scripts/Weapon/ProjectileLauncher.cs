using UnityEngine;
using Zenject;

namespace Asteroids
{
    public sealed class ProjectileLauncher : MonoBehaviour, IInputProxy
    {
        [SerializeField] private ProjectileData m_projectileData;
        [SerializeField] private Transform m_nozzle;

        private IInputInterface m_InputInterface;
        private Asteroids.Bullet.Factory m_bulletFactory;
        private float m_speed;
        private float m_timer;
        private float m_fireRate;

        public IInputInterface InputDependency
        {
            get => m_InputInterface;
            set => m_InputInterface = value;
        }

        [Inject]
        public void Setup(IInputInterface InputInterface, Bullet.Factory bulletFactory)
        {
            m_InputInterface = InputInterface;
            m_bulletFactory = bulletFactory;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_speed = m_projectileData.speed;
            m_fireRate = m_projectileData.fireRate;
        }

        private void Update()
        {
            if (m_InputInterface.Fire)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_fireRate)
            {
                m_timer = 0;
                SpawnBullet();
            }
        }

        private void SpawnBullet()
        {
            Bullet projectile = m_bulletFactory.Create(BulletType.FromPlayer);

            if (projectile != null)
            {
                projectile.transform.position = m_nozzle.position;
                projectile.transform.rotation = m_nozzle.rotation;
                projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                projectile.GetComponent<Rigidbody>().velocity = m_speed * Time.deltaTime * m_nozzle.up;
            }
        }
    }
}