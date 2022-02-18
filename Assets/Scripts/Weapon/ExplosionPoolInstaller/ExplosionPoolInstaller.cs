using UnityEngine;
using Zenject;
using Asteroids;


public class ExplosionPoolInstaller : MonoInstaller<DefaultInstaller>
{
    public Explosion explosion;
    public int Count;
    public override void InstallBindings()
    {
        Container.BindFactory<Explosion,Explosion.Factory>().
        FromPoolableMemoryPool<Explosion,ExplosionPool>(poolBinder => poolBinder.WithInitialSize(Count)
        .FromComponentInNewPrefab(explosion)
        .UnderTransformGroup("Explosions"));
    }

    class ExplosionPool : MonoPoolableMemoryPool<IMemoryPool,Explosion>
    {

    }
}
