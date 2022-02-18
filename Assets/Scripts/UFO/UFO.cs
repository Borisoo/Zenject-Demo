using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ModestTree;
using System;

namespace Asteroids
{

    public sealed class UFO : SpaceObjectBehaviour<BulletType, IProjectileInterface>, IPoolable<IMemoryPool>
    {
        private IMemoryPool _pool;
        private int score;
        private UFOData uFOData;
        private float _startTime;
        private INPCState currentState;
        private IScoreHandler _scoreHandler;


        [Inject]
        public void Construct(IScoreHandler scoreHandler, Bullet.Factory bulletFactory)
        {
            _scoreHandler = scoreHandler;
            _bulletFactory = bulletFactory;
        }

        [Inject] private UFOSpawnerSettings _settings;
        [Inject] private IShipInterface _playerShip;


        [Inject]
        [HideInInspector]
        public readonly UFOSettings ufoSettings;
        private Rigidbody _rigidBody;
        public override Vector3 Position { get => transform.position; set => transform.position = value; }

        //npc states
        public readonly UFOAttackState attackState = new UFOAttackState();
        public readonly UFORoamState roamState = new UFORoamState();
        public readonly UFOIdleState idleState = new UFOIdleState();

        public string currentStateName;
        public Bullet.Factory _bulletFactory;
        public int Score { set => score = value; }
        public IShipInterface PlayerShip { get => _playerShip; }
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

            currentState = roamState;
        }

        private void Update()
        {
            currentState = currentState.DoState(this);
            currentStateName = "" + currentState;
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
            _pool = pool;
            _startTime = Time.realtimeSinceStartup;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public override void SpawnExplosion()
        {
            base.SpawnExplosion();
        }

        private void UpdateScore()
        {
            _scoreHandler.UpdateScore(score);
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