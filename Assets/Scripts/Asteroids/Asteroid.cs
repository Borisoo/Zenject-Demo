using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using ModestTree;

namespace Asteroids
{

public sealed class Asteroid : SpaceObjectBehaviour<BulletType,IProjectileInterface> , IPoolable<IMemoryPool>
{
    private LevelHelper _level;
    [Inject] private AsteroidSpawnerSettings _settings;
    private float _startTime;
    private int numberOfFragments;
    private int score;
    private Vector3 scale;
    private AsteroidType asteroidType;
    public int Score{ set => score = value; }
    private AsteroidType fragmentType;
    public AsteroidType FragmentType{ set => fragmentType = value; }
    public Vector3 Scale { set => scale = value; }
    public int NumberOfFragments{ set => numberOfFragments = value; }



    IMemoryPool _pool;

    public AsteroidType AsteroidType
    {
        set => asteroidType = value;
    }
   
    [Inject]
    public void Construct( LevelHelper levelHelper,ISpawnerInterface<AsteroidType>  spawner,IScoreHandler  scoreHandler)
    {
        _level = levelHelper;
        _spawnerInterface = spawner;
        _scoreHandler = scoreHandler;
    }
    ISpawnerInterface<AsteroidType> _spawnerInterface;

    IScoreHandler _scoreHandler;

    public override Vector3 Position { get =>  transform.position; set => transform.position = value; }
    private Vector3 screenCenter;
    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Vector3 dir = Vector3.zero - transform.position;
        _rigidBody.AddForce(GetRandomDirection() * 100f);
    }

    Vector3 GetRandomDirection()
    {
        var theta = UnityEngine.Random.Range(0, Mathf.PI * 2.0f);
        return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
    }

    void Update()
    {
        if (Time.realtimeSinceStartup - _startTime > _settings.lifeTime)
        {
            _pool.Despawn(this);
        }

        Position = transform.position;
        CheckForTeleport();
    }
    
    public class Factory : PlaceholderFactory<Asteroid> {  }

    public override void Kill(BulletType type, IProjectileInterface projectile)
    {
        if(BulletType.FromPlayer == type)
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
        _scoreHandler.UpdateScore(score);
    }

    private void BreakIntoFragments()
    {
        if(!CanBreak())
        {
            SpawnExplosion();  
            return; 
        }

        for(int i =0; i < numberOfFragments; i++)
        {
            _spawnerInterface.SpawnAtPosition(fragmentType, this.transform.position);
        }        
    }

    private bool CanBreak()
    {
        if(asteroidType == AsteroidType.smallAsteroid || fragmentType == AsteroidType.none
        || asteroidType == AsteroidType.mediumAsteroid){ return false; }

        return true;
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

    private void CheckForTeleport()
    {
        if (Position.x > _level.Right + ScaleFactor && IsMovingInDirection(Vector3.right))
        {
            transform.SetX(_level.Left - ScaleFactor);
        }
        else if (Position.x < _level.Left - ScaleFactor && IsMovingInDirection(-Vector3.right))
        {
            transform.SetX(_level.Right + ScaleFactor);
        }
        else if (Position.y < _level.Bottom - ScaleFactor && IsMovingInDirection(-Vector3.up))
        {
            transform.SetY(_level.Top + ScaleFactor);
        }
        else if (Position.y > _level.Top + ScaleFactor && IsMovingInDirection(Vector3.up))
        {
            transform.SetY(_level.Bottom - ScaleFactor);
        }

        transform.RotateAround(transform.position, Vector3.up, 30 * Time.deltaTime);
    }

    internal bool IsMovingInDirection(Vector3 dir)
    {
        return Vector3.Dot(dir, _rigidBody.velocity) > 0;
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
            _rigidBody.mass = value;
        }
    }
}
}