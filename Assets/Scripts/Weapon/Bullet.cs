using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ModestTree;
using System;

namespace Asteroids
{
public sealed class Bullet : MonoBehaviour, IPoolable<BulletType,IMemoryPool>
{

    private float _startTime;
    IMemoryPool _pool;

    BulletType _type;
    public BulletType Type
    {
        get { return _type; }
    }

    [SerializeField]
    Material _playerMaterial = null;

    [SerializeField]
    Material _enemyMaterial = null;


    [Inject]
    BulletSettings _settings;

    private MeshRenderer _renderer;
   
    void Update()
    {
        if (Time.realtimeSinceStartup - _startTime > _settings.LifeTime)
        {
            _pool.Despawn(this);
        }
    }

    public void OnSpawned(BulletType type,IMemoryPool pool)
    {
        _pool = pool;
        _type = type;

        _renderer = GetComponent<MeshRenderer>();

        _renderer.material = type == BulletType.FromEnemy ? _enemyMaterial : _playerMaterial;
        _startTime = Time.realtimeSinceStartup;
    }

    void OnTriggerEnter(Collider other)
    {
        if(_type == BulletType.FromEnemy)
        {
            if(other.gameObject.TryGetComponent(out ShipBehaviour ship))
            {
                ship.Kill();
                this.gameObject.SetActive(false);
            }
        }

        if(_type == BulletType.FromPlayer)
        {
            if(other.gameObject.TryGetComponent(out Asteroid asteroid))
            {
                asteroid.Kill();
                this.gameObject.SetActive(false);
            }

            if(other.gameObject.TryGetComponent(out UFO ufo))
            {
                ufo.Kill();
                this.gameObject.SetActive(false);
            }
        }
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    void OnDisable()
    {
        if(_pool!=null)
        _pool.Despawn(this);
    }
    public class Factory : PlaceholderFactory<BulletType,Bullet> {  }
}
}