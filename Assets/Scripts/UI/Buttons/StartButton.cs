
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids
{
    public class StartButton : MonoBehaviour
    {
        private Button m_startButton;
        [Inject]private IGameController m_gameController;

        void Start()
        {
            m_startButton = GetComponent<Button>();
            m_startButton.onClick.AddListener(()=> m_gameController.StartGame());
        }
    }
}
