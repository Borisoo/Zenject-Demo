using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Asteroids
{
public class InputManager : IInputInterface
{
    private float _horizontal;
    private float _vertical;
    private bool _Fire;
    private UnityAction _FireAction;

    public float Horizontal{ get => GetTurnAxis();   }

    public float Vertical{get => GetForwardThrust(); }

    public bool Fire{ get => IsShooting();  }

    public UnityAction FireAction
    {
        get => _FireAction;
        set => _FireAction = value;
    }

    private float GetForwardThrust()
    {
        return  _vertical = Input.GetAxis("Vertical");
    }

    public float GetTurnAxis()
    {
        return  _horizontal = Input.GetAxis("Horizontal");
    }

    public bool IsShooting()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public  bool IsHyperspacing()
    {
        return Input.GetButtonDown("Fire2");
    }
}
}