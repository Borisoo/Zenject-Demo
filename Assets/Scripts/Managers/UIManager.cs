using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public sealed class UIManager : MonoBehaviour, IUIManagerInterface
    {
        [Inject]
        INavigationControllerInterface NavigationController;

        [SerializeField] GameObject HomeScreen;
        [SerializeField] GameObject GameScreen;
        [SerializeField] GameObject EndScreen;

        void Start()
        {
           NavigationController.Push(HomeScreen,true,true);
        }
        
        public void ShowGameScreen()
        {
            NavigationController.Push(GameScreen,false,true);
        }
        
        public void ShowEndScreen()
        {
            NavigationController.Push(EndScreen,false,true);
        }
    }
}