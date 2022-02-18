using UnityEngine;
using Zenject;
using ModestTree;

namespace Asteroids
{

    public sealed class UFO : SpaceObjectBehaviour<BulletType, IProjectileInterface>, IPoolable<IMemoryPool>
    {
        private IMemoryPool m_pool;
        private int m_score;
        private UFOData m_ufoData;
        private float m_startTime;
        private INPCState m_currentState;
        private IScoreHandler m_scoreHandler;



        [Inject] private UFOSpawnerSettings _settings;
        [Inject] private IShipInterface _playerShip;


        [Inject]
        [HideInInspector]
        public readonly UFOSettings UfoSettings;
        private Rigidbody _rigidBody;
        public readonly UFOAttackState attackState = new UFOAttackState();
        public readonly UFORoamState roamState = new UFORoamState();
        public readonly UFOIdleState idleState = new UFOIdleState();
        public Bullet.Factory _bulletFactory;
        public int Score { set => m_score = value; }
        public IShipInterface PlayerShip { get => _playerShip; }
        [Inject]
        public void Construct(IScoreHandler scoreHandler, Bullet.Factory bulletFactory)
        {
            m_scoreHandler = scoreHandler;
            _bulletFactory = bulletFactory;
        }

        public override Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public Vector3 RightDir()
        {
            return transform.right;
        }

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();

            attackState.SetNPC = this;
            roamState.SetNPC = this;
            idleState.SetNPC = this;

            m_currentState = roamState;
        }

        private void Update()
        {
            m_currentState = m_currentState.DoState(this);
        }

        public override void Kill(BulletType type, IProjectileInterface projectile)
        {
            if (BulletType.FromPlayer == type)
            {
                SpawnExplosion();
                UpdateScore();
                projectile.DestroyProjectile();
                this.gameObject.SetActive(false);
            }
        }

        public void OnSpawned(IMemoryPool pool)
        {
            m_pool = pool;
            m_startTime = Time.realtimeSinceStartup;
        }

        public void OnDespawned()
        {
            m_pool = null;
        }

        public override void SpawnExplosion()
        {
            base.SpawnExplosion();
        }

        private void UpdateScore()
        {
            m_scoreHandler.UpdateScore(m_score);
        }

        public float Scale
        {
            get
            {
                var scale = transform.localScale;
                Assert.That(scale[0] == scale[1] && scale[1] == scale[2]);
                return scale[0];
            }
            set
            {
                transform.localScale = new Vector3(value, value, value);
                _rigidBody.mass = value;
            }
        }

        public class Factory : PlaceholderFactory<UFO> { }
    }
}