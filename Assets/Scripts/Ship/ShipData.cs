
using UnityEngine;


[CreateAssetMenu(menuName = "ShipData", order = 51)]
public class ShipData : ScriptableObject
{
    public float thrustSpeed;
    public float rotateSpeed;
}

public interface IShipDataProxy
{
    ShipData shipData { get; set; }
}