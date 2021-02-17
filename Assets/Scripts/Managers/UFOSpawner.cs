using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using ModestTree;

namespace Asteroids
{
public sealed class UFOSpawner : ITickable, ISpawnerInterface<UFOType>
{
    private UFO.Factory _UFOFactory;
    private LevelHelper _level;
    private IUFOFactoryInterface<UFOType,UFOData> UFODataFactory;
    private float _timer;

    [Inject]
    private UFOSpawnerSettings _settings;

    bool canSpawn;


    [Inject]
    public void Construct(UFO.Factory ufoFactory, LevelHelper level, IUFOFactoryInterface<UFOType,UFOData> ufoDataFactoryInterface)
    {
        _UFOFactory = ufoFactory;
        _level = level;
         UFODataFactory = ufoDataFactoryInterface;
    }
    public void Start()
    {
        canSpawn = true;
    }

    public void Tick()
    {
        SpawnUFOAtInterval();
    }

    private void SpawnUFOAtInterval()
    {
        if(!canSpawn){ return; }

        _timer += Time.deltaTime;
        if(_timer > _settings.spawnDelay)
        {
            _timer = 0;
           SpawnUFO();
        }
    }

    private void SpawnUFO()
    {
        UFO ufo = _UFOFactory.Create(); 

        var ufoType = RandomEnum<UFOType>.Get(); 
        var score = UFODataFactory.GetScore(ufoType);
        var ufoData = UFODataFactory.GetData(ufoType);

        ufo.Score = score;
        ufo.transform.position = GetRandomStartPosition(ufo.Scale); 
    }

    Vector3 GetRandomStartPosition(float scale)
    {
        var area = RandomEnum<AreaType>.Get();
        var rand = UnityEngine.Random.Range(0.0f, 1.0f);

        switch (area)
        {
            case AreaType.top:
            {
                return new Vector3(_level.Left + rand * _level.Width, _level.Top + scale, 0);
            }
            case AreaType.bottom:
            {
                return new Vector3(_level.Left + rand * _level.Width, _level.Bottom - scale, 0);
            }
            case AreaType.right:
            {
                return new Vector3(_level.Right + scale, _level.Bottom + rand * _level.Height, 0);
            }
            case AreaType.left:
            {
                return new Vector3(_level.Left - scale, _level.Bottom + rand * _level.Height, 0);
            }
        }

        throw Assert.CreateException();
    }

    public void SpawnAtPosition(UFOType type, Vector3 pos)
    {

    }

    public void Stop()
    {
        canSpawn = false;
    }
}
}
