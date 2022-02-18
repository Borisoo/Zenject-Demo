using UnityEngine;
using UnityEngine.Events;

namespace Asteroids
{
    public class InputManager : IInputInterface
    {
        private float m_horizontal;
        private float m_vertical;
        private bool m_fire;
        private UnityAction m_FireAction;
        public float Horizontal { get => GetTurnAxis(); }
        public float Vertical { get => GetForwardThrust(); }
        public bool Fire { get => IsShooting(); }

        public UnityAction FireAction
        {
            get => m_FireAction;
            set => m_FireAction = value;
        }

        private float GetForwardThrust()
        {
            return m_vertical = Input.GetAxis("Vertical");
        }

        public float GetTurnAxis()
        {
            return m_horizontal = Input.GetAxis("Horizontal");
        }

        public bool IsShooting()
        {
            return Input.GetKey(KeyCode.Space);
        }

        public bool IsHyperspacing()
        {
            return Input.GetButtonDown("Fire2");
        }
    }
}