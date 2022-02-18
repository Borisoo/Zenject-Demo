﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public sealed class ProjectileLauncher : MonoBehaviour, IInputProxy
    {
        [SerializeField] private ProjectileData m_projectileData;
        [SerializeField] private Transform m_nozzle;

        private IInputInterface _InputInterface;
        private Asteroids.Bullet.Factory _bulletFactory;
        private float _speed;
        private float _timer;
        private float _fireRate;

        public IInputInterface InputDependency
        {
            get => _InputInterface;
            set => _InputInterface = value;
        }

        [Inject]
        public void Setup(IInputInterface InputInterface, Bullet.Factory bulletFactory)
        {
            _InputInterface = InputInterface;
            _bulletFactory = bulletFactory;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _speed = m_projectileData.speed;
            _fireRate = m_projectileData.fireRate;
        }

        private void Update()
        {
            if (_InputInterface.Fire)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _timer += Time.deltaTime;
            if (_timer > _fireRate)
            {
                _timer = 0;
                SpawnBullet();
            }
        }

        private void SpawnBullet()
        {
            Bullet projectile = _bulletFactory.Create(BulletType.FromPlayer);

            if (projectile != null)
            {
                projectile.transform.position = m_nozzle.position;
                projectile.transform.rotation = m_nozzle.rotation;
                projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                projectile.GetComponent<Rigidbody>().velocity = _speed * Time.deltaTime * m_nozzle.up;
            }
        }
    }
}