using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ModestTree;
using System;

namespace Asteroids
{
public sealed class AsteroidSpawner : ITickable, ISpawnerInterface<AsteroidType>
{
    private LevelHelper _level;
 
    IAsteroidFactoryInterface<AsteroidType,AsteroidData>  asteroidDataFactoryInterface;

    [Inject]
    public void Construct(IAsteroidFactoryInterface<AsteroidType,AsteroidData> asteroidDataFactoryInterface,
    LevelHelper levelHelper)
    {
        this.asteroidDataFactoryInterface = asteroidDataFactoryInterface;
        this._level = levelHelper;
    }

    [Inject]
    private Asteroid.Factory asteroidFactory;

    [Inject]
    private UFO.Factory ufoFactory;

    [Inject]
    private AsteroidSpawnerSettings settings;

    private float _timer;
    bool canSpawn;
 
    public void Tick()
    {
      SpawnAsteroidAtInterval();
    }

    private void SpawnAsteroidAtInterval()
    {
        if(!canSpawn){ return; }

        _timer += Time.deltaTime;
        if(_timer > settings.spawnDelay)
        {
            _timer = 0;
            SpawnAsteroid();
        }
    }
    public void Start()
    {
        canSpawn = true;
    }

    private void SpawnAsteroid()
    {
        var random_AsteroidType = RandomEnum<AsteroidType>.Get(); 
        var data = asteroidDataFactoryInterface.GetData(random_AsteroidType);

        if(data == null || random_AsteroidType == AsteroidType.none)
        {
           return;
        }

        Asteroid asteroid = asteroidFactory.Create(); 

        InitializeAsteroid(ref asteroid,data,random_AsteroidType);
        asteroid.transform.position = GetRandomStartPosition(asteroid.ScaleFactor); 
    }

    /// <summary>
    /// spawn asteroid at given point
    /// </summary>
    /// <param name="type"></param>
    /// <param name="position"></param>    
    public void SpawnAtPosition(AsteroidType type, Vector3 position)
    {
        Asteroid asteroid = asteroidFactory.Create(); 

        asteroid.transform.position = position;

        var data = asteroidDataFactoryInterface.GetData(type);
        InitializeAsteroid(ref asteroid,data,type);
    }

  
    public void InitializeAsteroid(ref Asteroid asteroid, AsteroidData data, AsteroidType type)
    {
        asteroid.transform.localScale = data.scale;
        asteroid.NumberOfFragments = data.numberOfFragments;
        asteroid.Score = data.scorePoints;
        asteroid.AsteroidType = type;
        asteroid.FragmentType = data.fragmentType;
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

    public void Stop()
    {
        canSpawn = false;
    }
}

}