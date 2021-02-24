using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids
{
    public class ReloadButton : MonoBehaviour
    {
        private  Button reloadButton;
        [Inject]private IGameController gameController;

        void Start()
        {
            reloadButton = GetComponent<Button>();
            reloadButton.onClick.AddListener(()=> gameController.Reload());
        }
    }
}
