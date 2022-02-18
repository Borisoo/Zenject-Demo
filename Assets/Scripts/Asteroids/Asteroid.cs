using UnityEngine;
using Zenject;
using System;
using ModestTree;

namespace Asteroids
{
    public sealed class Asteroid : SpaceObjectBehaviour<BulletType, IProjectileInterface>, IPoolable<IMemoryPool>
    {
        private LevelHelper m_levelHelper;
        [Inject] private AsteroidSpawnerSettings m_asteroidSpanwerSettings;
        private float m_startTime;
        private int m_numberOfFragments;
        private int m_score;
        private Vector3 m_scale;
        private AsteroidType m_asteroidType;
        private IMemoryPool m_pool;
        private AsteroidType m_fragmentType;
        private ISpawnerInterface<AsteroidType> m_spawnerInterface;
        private IScoreHandler m_scoreHandler;
        private Rigidbody m_rigidBody;

        public int Score { set => m_score = value; }
        public int NumberOfFragments { set => m_numberOfFragments = value; }
        public AsteroidType FragmentType { set => m_fragmentType = value; }
        public AsteroidType AsteroidType { set => m_asteroidType = value; }
        public override Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        [Inject]
        public void Construct(LevelHelper levelHelper, ISpawnerInterface<AsteroidType> spawner, IScoreHandler scoreHandler)
        {
            m_levelHelper = levelHelper;
            m_spawnerInterface = spawner;
            m_scoreHandler = scoreHandler;
        }

        private void Start()
        {
            m_rigidBody = GetComponent<Rigidbody>();
            Vector3 dir = Vector3.zero - transform.position;
            m_rigidBody.AddForce(GetRandomDirection() * 100f);
        }

        private Vector3 GetRandomDirection()
        {
            var theta = UnityEngine.Random.Range(0, Mathf.PI * 2.0f);
            return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
        }

        private void Update()
        {
            if (Time.realtimeSinceStartup - m_startTime > m_asteroidSpanwerSettings.lifeTime)
            {
                m_pool.Despawn(this);
            }

            Position = transform.position;
            CheckForTeleport();
        }

        public override void Kill(BulletType type, IProjectileInterface projectile)
        {
            if (BulletType.FromPlayer == type)
            {
                SpawnExplosion();
                UpdateScore();
                BreakIntoFragments();
                projectile.DestroyProjectile();
                this.gameObject.SetActive(false);
            }
        }

        private void UpdateScore()
        {
            m_scoreHandler.UpdateScore(m_score);
        }

        private void BreakIntoFragments()
        {
            if (!CanBreak())
            {
                SpawnExplosion();
                return;
            }

            for (int i = 0; i < m_numberOfFragments; i++)
            {
                m_spawnerInterface.SpawnAtPosition(m_fragmentType, this.transform.position);
            }
        }

        private bool CanBreak()
        {
            if (m_asteroidType == AsteroidType.smallAsteroid || m_fragmentType == AsteroidType.none
            || m_asteroidType == AsteroidType.mediumAsteroid) { return false; }

            return true;
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

        private void CheckForTeleport()
        {
            if (Position.x > m_levelHelper.Right + ScaleFactor && IsMovingInDirection(Vector3.right))
            {
                transform.SetX(m_levelHelper.Left - ScaleFactor);
            }
            else if (Position.x < m_levelHelper.Left - ScaleFactor && IsMovingInDirection(-Vector3.right))
            {
                transform.SetX(m_levelHelper.Right + ScaleFactor);
            }
            else if (Position.y < m_levelHelper.Bottom - ScaleFactor && IsMovingInDirection(-Vector3.up))
            {
                transform.SetY(m_levelHelper.Top + ScaleFactor);
            }
            else if (Position.y > m_levelHelper.Top + ScaleFactor && IsMovingInDirection(Vector3.up))
            {
                transform.SetY(m_levelHelper.Bottom - ScaleFactor);
            }

            transform.RotateAround(transform.position, Vector3.up, 30 * Time.deltaTime);
        }

        internal bool IsMovingInDirection(Vector3 dir)
        {
            return Vector3.Dot(dir, m_rigidBody.velocity) > 0;
        }

        public float ScaleFactor
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
                m_rigidBody.mass = value;
            }
        }

        public class Factory : PlaceholderFactory<Asteroid> { }
    }
}