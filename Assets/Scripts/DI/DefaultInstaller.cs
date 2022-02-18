using UnityEngine;
using Zenject;
using Asteroids;

public class DefaultInstaller : MonoInstaller<DefaultInstaller>
{
    public ShipBehaviour shipBehaviour;

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

    }
}