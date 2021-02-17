using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ModestTree;
using System;

namespace Asteroids
{

public sealed class UFO : SpaceObjectBehaviour, IPoolable<IMemoryPool>, INPCInterface
{
    IMemoryPool _pool;
    private int score;
    private UFOData uFOData;
    public UFOData SetUFOData{ set => uFOData = value;}
    public int Score{ set => score = value; }
    private float _startTime;
    IScoreHandler _scoreHandler;
    public  Bullet.Factory _bulletFactory;
    [Inject]
    public void Construct(IScoreHandler  scoreHandler, Bullet.Factory bulletFactory)
    {
       _scoreHandler = scoreHandler;
       _bulletFactory = bulletFactory;
    }

    [Inject]
    private UFOSpawnerSettings _settings;
    [Inject]
    private IShipInterface _playerShip;
    public IShipInterface PlayerShip { get => _playerShip; }

    [Inject]
    [HideInInspector]
    public UFOSettings ufoSettings;
    private Rigidbody _rigidBody;
    public override Vector3 Position { get =>  transform.position; set => transform.position = value; }

    //npc states
    public UFOAttackState attackState = new UFOAttackState();
    public UFORoamState roamState = new UFORoamState();
    public UFOIdleState idleState = new UFOIdleState();
    public INPCState currentState;
    public string currentStateName;

    public Vector3 RightDir()
    {
        return transform.right;
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        attackState.SetNPC = this;
        roamState.SetNPC = this; 
        idleState.SetNPC = this;
  
        currentState = roamState;
    }

    void Update()
    {
        currentState = currentState.DoState(this);
        currentStateName = "" + currentState;
    }

    public void Kill()
    {
        SpawnExplosion();
        UpdateScore();
        
        this.gameObject.SetActive(false);
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

    public class Factory : PlaceholderFactory<UFO> {  }
}

}