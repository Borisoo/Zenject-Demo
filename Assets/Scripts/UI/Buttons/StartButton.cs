using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids
{
    public class StartButton : MonoBehaviour
    {
        private Button startButton;
        [Inject]private  IGameController gameController;

        void Start()
        {
            startButton = GetComponent<Button>();
            startButton.onClick.AddListener(()=> gameController.StartGame());
        }
    }
}
