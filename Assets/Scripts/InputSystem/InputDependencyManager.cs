
using UnityEngine;
using System.Linq;


namespace Asteroids
{
    public sealed class InputSystemDependencyManager : MonoBehaviour
    {
        [RequireInterface(typeof(IInputInterface))]
        public UnityEngine.Object inputSystem;
        public IInputInterface m_inputInterface => inputSystem as IInputInterface;
        void Awake()
        {
            UpdateRootDependency();
        }
        void UpdateRootDependency()
        {
            var components = this.gameObject.GetComponents<MonoBehaviour>();
            var dependents = components.Where(c => c is IInputProxy)
            .Cast<IInputProxy>();

            foreach (var dependent in dependents)
            {
                dependent.InputDependency = m_inputInterface;
            }
        }
    }
}