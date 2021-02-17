using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

namespace Asteroids
{
    public class SpaceObjectBehaviour : MonoBehaviour
    {
        public virtual Vector3 Position{ get; set; }
       
        [Inject]
        public Explosion.Factory ExplosionFactory;

        public virtual void SpawnExplosion()
        {
            try
            {
                var explosion = ExplosionFactory.Create();
                explosion.transform.position = Position;
            }
            catch
            {
                Debug.Log("object position missing");
            }
        }
    }
}