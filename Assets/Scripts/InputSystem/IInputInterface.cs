using UnityEngine.Events;

public interface IInputInterface
{
    float Horizontal{ get;}
    float Vertical{get; }
    bool Fire{ get;  }
    UnityAction FireAction{ get;set;}
}