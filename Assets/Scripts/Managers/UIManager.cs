using UnityEngine;
using Zenject;

namespace Asteroids
{
    public sealed class UIManager : MonoBehaviour, IUIManagerInterface
    {
        [Inject] private INavigationControllerInterface NavigationController;

        [SerializeField] private GameObject HomeScreen;
        [SerializeField] private GameObject GameScreen;
        [SerializeField] private GameObject EndScreen;

        private void Start()
        {
            NavigationController.Push(HomeScreen, true, true);
        }

        public void ShowGameScreen()
        {
            NavigationController.Push(GameScreen, false, true);
        }

        public void ShowEndScreen()
        {
            NavigationController.Push(EndScreen, false, true);
        }
    }
}