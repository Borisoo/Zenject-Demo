using UnityEngine;
using Zenject;
using Asteroids;

public class DefaultInstaller : MonoInstaller<DefaultInstaller>
{
    public ShipBehaviour shipBehaviour;
    public Asteroid asteroid;
    public Asteroids.UFO uFO;
    public Bullet bullet;
    public Explosion explosion;

    public override void InstallBindings()
    {
        Container.Bind<IShipInterface>().FromComponentInNewPrefab(shipBehaviour).AsSingle();

        Container.Bind<Asteroids.LevelHelper>().AsSingle();

        Container.Bind<IInputInterface>().To<InputManager>().AsSingle();

        Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();

        Container.Bind<IScoreHandler>().To<ScoreManager>().AsSingle();

        Container.Bind<IAsteroidFactoryInterface<AsteroidType,AsteroidData>>().To<AsteroidDataFactory>().AsSingle();

        Container.Bind<IUFOFactoryInterface<UFOType,UFOData>>().To<UFODataFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<UFOSpawner>().AsSingle();

        Container.Bind<INavigationControllerInterface>().To<UINavigationController>().AsSingle();

        Container.Bind<IGameController>().To<GameController>().AsSingle();

        //-----------------------------------------------> Bind Factories

        Container.BindFactory<Asteroid,Asteroid.Factory>().
        FromPoolableMemoryPool<Asteroid,AsteroidPool>(poolBinder => poolBinder.WithInitialSize(10)
        .FromComponentInNewPrefab(asteroid)
        .UnderTransformGroup("Asteroids"));

        Container.BindFactory<BulletType,Bullet,Bullet.Factory>().
        FromPoolableMemoryPool<BulletType,Bullet,BulletPool>(poolBinder => poolBinder.WithInitialSize(20)
        .FromComponentInNewPrefab(bullet)
        .UnderTransformGroup("Bullets"));

        Container.BindFactory<Explosion,Explosion.Factory>().
        FromPoolableMemoryPool<Explosion,ExplosionPool>(poolBinder => poolBinder.WithInitialSize(10)
        .FromComponentInNewPrefab(explosion)
        .UnderTransformGroup("Explosions"));

        Container.BindFactory<UFO,UFO.Factory>().
        FromPoolableMemoryPool<UFO,UFOPool>(poolBinder => poolBinder.WithInitialSize(10)
        .FromComponentInNewPrefab(uFO)
        .UnderTransformGroup("ufo"));

    }

    class AsteroidPool : MonoPoolableMemoryPool<IMemoryPool,Asteroid>
    {

    }
    class BulletPool: MonoPoolableMemoryPool<BulletType,IMemoryPool,Bullet>
    {

    }
    class ExplosionPool : MonoPoolableMemoryPool<IMemoryPool,Explosion>
    {

    }
    class UFOPool : MonoPoolableMemoryPool<IMemoryPool, UFO>
    {

    }
}