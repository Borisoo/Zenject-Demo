using UnityEngine;
using Zenject;
using Asteroids;


public class UfoPoolInstaller : MonoInstaller<DefaultInstaller>
{
    public Asteroids.UFO uFO;
    public int Count;
    public override void InstallBindings()
    {
        Container.BindFactory<UFO, UFO.Factory>().
        FromPoolableMemoryPool<UFO, UFOPool>(poolBinder => poolBinder.WithInitialSize(Count)
        .FromComponentInNewPrefab(uFO)
        .UnderTransformGroup("ufo"));
    }
    class UFOPool : MonoPoolableMemoryPool<IMemoryPool, UFO>
    {

    }
}
