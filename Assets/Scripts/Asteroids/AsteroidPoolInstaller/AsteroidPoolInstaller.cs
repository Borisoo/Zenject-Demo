using UnityEngine;
using Zenject;
using Asteroids;

public class AsteroidPoolInstaller : MonoInstaller<DefaultInstaller>
{
    public Asteroid Asteroid;
    public int Count;
    public override void InstallBindings()
    {
        Container.BindFactory<Asteroid, Asteroid.Factory>().
        FromPoolableMemoryPool<Asteroid, AsteroidPool>(poolBinder => poolBinder.WithInitialSize(Count)
        .FromComponentInNewPrefab(Asteroid)
        .UnderTransformGroup("Asteroids"));
    }

    class AsteroidPool : MonoPoolableMemoryPool<IMemoryPool, Asteroid>
    {

    }
}
