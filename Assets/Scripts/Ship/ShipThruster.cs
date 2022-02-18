using UnityEngine;
using Zenject;

namespace Asteroids
{
    public sealed class ShipThruster : MonoBehaviour, IInputProxy
    {
        [SerializeField] private ShipData m_shipData;
        private Rigidbody m_rigidBody;
        private float m_speed;
        private float m_rotateSpeed;
        private IInputInterface m_inputInterface;
        public IInputInterface InputDependency
        {
            get => m_inputInterface;
            set => m_inputInterface = value;
        }

        [Inject]
        public void Setup(IInputInterface inputInterface)
        {
            m_inputInterface = inputInterface;
        }

        private  void Start()
        {
            Initialize();
            m_rigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Initialize()
        {
            m_speed = m_shipData.thrustSpeed;
            m_rotateSpeed = m_shipData.rotateSpeed;
        }

        private void Move()
        {
            if (m_inputInterface.Vertical != 0)
            {
                Vector3 thrustForce = m_inputInterface.Vertical * transform.up * m_speed * Time.deltaTime; ;
                m_rigidBody.velocity = thrustForce;
            }
        }

        private void Rotate()
        {
            float turn = m_inputInterface.Horizontal * Time.deltaTime * m_rotateSpeed;
            Vector3 torque = transform.forward * -turn;
            transform.Rotate(torque);
        }
    }
}