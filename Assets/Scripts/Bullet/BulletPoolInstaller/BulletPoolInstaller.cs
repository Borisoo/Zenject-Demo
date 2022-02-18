
using UnityEngine;
using Zenject;
using Asteroids;


public class BulletPoolInstaller : MonoInstaller<DefaultInstaller>
{
    public Bullet bullet;
    public int Count;
    public override void InstallBindings()
    {
        Container.BindFactory<BulletType, Bullet, Bullet.Factory>().
        FromPoolableMemoryPool<BulletType, Bullet, BulletPool>(poolBinder => poolBinder.WithInitialSize(Count)
        .FromComponentInNewPrefab(bullet)
        .UnderTransformGroup("Bullets"));
    }

    class BulletPool : MonoPoolableMemoryPool<BulletType, IMemoryPool, Bullet>
    {

    }
}
