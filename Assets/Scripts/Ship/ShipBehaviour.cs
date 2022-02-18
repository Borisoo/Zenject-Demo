using UnityEngine;
using Zenject;

namespace Asteroids
{
    public sealed class ShipBehaviour : SpaceObjectBehaviour<BulletType, IProjectileInterface>, IShipInterface
    {
        private IInputInterface m_inputInterface;
        private IGameController m_gameController;
        private bool m_isDead;

        [Inject]
        public void Setup(IInputInterface inputInterface, IGameController gameController)
        {
            m_inputInterface = inputInterface;
            m_gameController = gameController;
        }

        public GameObject TargetObject { get => this.gameObject; }
        public Vector3 ShipPosition { get => this.transform.position; }
        public override Vector3 Position { get => this.transform.position; }
        public bool IsDead => m_isDead;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Obstacle))
            {
                InvokeDeath();
            }
        }

        public override void Kill(BulletType type, IProjectileInterface projectile)
        {
            if (BulletType.FromEnemy == type)
            {
                InvokeDeath();
                projectile.DestroyProjectile();
            }
        }

        public void InvokeDeath()
        {
            SpawnExplosion();
            m_gameController.EndGame();
            m_isDead = true;
            gameObject.SetActive(false);
        }

        public override void SpawnExplosion()
        {
            base.SpawnExplosion();
        }
    }
}