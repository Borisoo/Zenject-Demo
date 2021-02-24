using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ModestTree;
using System;

namespace Asteroids
{
public sealed class Bullet : MonoBehaviour, IPoolable<BulletType,IMemoryPool>, IProjectileInterface
{
    private float _startTime;
    private IMemoryPool _pool;
    private  BulletType _type;
    
    [SerializeField]private  Material _playerMaterial = null;
    [SerializeField]private Material _enemyMaterial = null;

    [Inject]
    private BulletSettings _settings;
    private MeshRenderer _renderer;
   
    public BulletType Type {  get { return _type; } }


    private void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out SpaceObjectBehaviour<BulletType,IProjectileInterface> ship))
        {
            ship.Kill(_type,this);
        }
    }

    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }


    public void OnDespawned()
    {
        _pool = null;
    }

    private void OnDisable()
    {
        if(_pool!=null)
        _pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<BulletType,Bullet> {  }
}
}