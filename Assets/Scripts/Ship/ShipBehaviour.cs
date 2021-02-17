using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
public sealed class ShipBehaviour : SpaceObjectBehaviour<BulletType,IProjectileInterface>, IInputProxy, IShipInterface
{
    private IInputInterface _inputInterface;
    public IInputInterface InputDependency{ get => _inputInterface; set => _inputInterface = value; }
    private IGameController _gameController;
    
    [Inject]
    public void Setup(IInputInterface inputInterface, IGameController gameController)
    {
        _inputInterface = inputInterface;
        _gameController = gameController;
    }

    public GameObject TargetObject{ get => this.gameObject; }
    public Vector3 ShipPosition{ get => this.transform.position; }
    public override Vector3 Position{ get => this.transform.position; }
    public bool IsDead => isDead;
    private bool isDead;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Tags.Obstacle))
        {
           InvokeDeath();
        }
    }

    public override void Kill(BulletType type,IProjectileInterface projectile)
    {
        if(BulletType.FromEnemy == type)
        {
            InvokeDeath();
            projectile.DestroyProjectile();
        }
    }

    public void InvokeDeath()
    {
        SpawnExplosion();
        _gameController.EndGame();
       isDead = true;
       gameObject.SetActive(false);
    }

    public override void SpawnExplosion()
    {
       base.SpawnExplosion();
    }
}
}