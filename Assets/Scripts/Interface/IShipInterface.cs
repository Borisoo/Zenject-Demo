using UnityEngine;

namespace Asteroids
{
public interface IShipInterface
{
    bool IsDead{get;}
    GameObject TargetObject{ get;}
    Vector3 ShipPosition{ get; }
}
}