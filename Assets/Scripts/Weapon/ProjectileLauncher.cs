using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
public sealed class ProjectileLauncher : MonoBehaviour , IInputProxy
{
   [SerializeField] private ProjectileData projectileData;
   [SerializeField] private Transform Nozzle;  
   private IInputInterface _InputInterface; 
   private Asteroids.Bullet.Factory _bulletFactory;
   private float speed;
   private float timer;
   private float fireRate;
   private int pooledObjectIndex; 
   public IInputInterface InputDependency{get => _InputInterface;  set => _InputInterface = value; }
   
    [Inject]
    public void Setup(IInputInterface InputInterface,  Bullet.Factory bulletFactory)
    {
        _InputInterface = InputInterface;
        _bulletFactory = bulletFactory;
    }

   void Start()
   {
       Initialize();
   }

   private void Initialize()
   {
       speed = projectileData.speed;
       fireRate = projectileData.fireRate;
   }

   void Update()
   {
       if(_InputInterface.Fire)
       {
           Shoot();
       }
   }

   private void Shoot()
   {
       timer += Time.deltaTime;
       if(timer > fireRate)
       {
           timer = 0;
           SpawnBullet();
       }
   }

   private void SpawnBullet()
   {
       Bullet projectile = _bulletFactory.Create(BulletType.FromPlayer);

       if(projectile!=null)
       {    
           projectile.transform.position = Nozzle.position;
           projectile.transform.rotation = Nozzle.rotation;
           projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
           projectile.GetComponent<Rigidbody>().velocity = speed * Time.deltaTime * Nozzle.up;
       }
   }
}
}