using UnityEngine;


[CreateAssetMenu(menuName = "BoundsData", order = 51)]
public class BoundsData : ScriptableObject
{
    public float MaxX;
    public float MinX;
    public float MaxY;
    public float MinY;
}

public interface IBoundsDataProxy
{
    BoundsData boundsData { get; set; }
}