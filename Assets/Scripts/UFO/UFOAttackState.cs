using UnityEngine;
using Zenject;

namespace Asteroids
{
public class UFOAttackState : INPCState
{
    private UFO ufo;
    private float timer;
    private float _strafeTimer;
    private bool _strafeRight;

    public UFO SetNPC 
    {
        set => ufo = value;
    }

    public INPCState DoState( UFO npc)
    {
        if(npc.PlayerShip.IsDead){ return npc.idleState; }

        var lookDir = (npc.PlayerShip.ShipPosition - npc.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(lookDir, Vector3.forward);

        npc.transform.rotation = targetRotation;

        Strafe();

        timer += Time.deltaTime;
        if(timer > npc.ufoSettings.attackInterval)
        {
            timer = 0;
            Fire(lookDir);
        }
        
        return npc.attackState;
    }

    private void Strafe()
    {
        _strafeTimer += Time.deltaTime;
        if (_strafeTimer > ufo.ufoSettings.strafeInterval)
        {
            _strafeRight = !_strafeRight;
        }

        if (_strafeRight)
        {
           ufo.transform.position += ufo.RightDir() * Time.deltaTime  * ufo.ufoSettings.strafeSpeed;
        }
        else
        {
            ufo.transform.position += -ufo.RightDir() * Time.deltaTime  * ufo.ufoSettings.strafeSpeed;
        }
    }

    private void Fire(Vector3 dir)
    {
       Bullet projectile = ufo._bulletFactory.Create(BulletType.FromEnemy);

       if(projectile!=null)
       {    
           projectile.transform.position = ufo.transform.position;
           projectile.transform.rotation = ufo.transform.rotation;

           projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
           projectile.GetComponent<Rigidbody>().AddForce( ufo.ufoSettings.attackSpeed * dir) ;
       }
    }   
}
}