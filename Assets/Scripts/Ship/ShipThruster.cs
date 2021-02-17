using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
public sealed class ShipThruster : MonoBehaviour, IInputProxy, IShipDataProxy
{
    public ShipData _shipData;
    private float speed;
    private float rotateSpeed;

    //bounds
    private float MaxX,MinX;
    private float MinY,MaxY;

    private IInputInterface m_inputInterface;

    public IInputInterface InputDependency{ get => m_inputInterface; set => m_inputInterface = value; }

    public ShipData shipData{ get => _shipData; set => _shipData = value; }

    private Rigidbody rigidbody;

    [Inject]
    public void Setup(IInputInterface inputInterface)
    {
        m_inputInterface = inputInterface;
    }

    void Start()
    {
        Initialize();
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Initialize()
    {
        speed = shipData.thrustSpeed;
        rotateSpeed = shipData.rotateSpeed;
    }

    void Move()
    {
        if(m_inputInterface.Vertical != 0)
        {
            Vector3 thrustForce = m_inputInterface.Vertical * transform.up * speed  * Time.deltaTime;;  
            rigidbody.velocity = thrustForce;
        }
    }

    void Rotate()
    {
        float turn = m_inputInterface.Horizontal * Time.deltaTime * rotateSpeed;
        Vector3 torque = transform.forward * -turn;
        transform.Rotate(torque);
    }
}
}