
using Zenject;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Asteroids
{
    public class GameController : IGameController
    {
        [Inject] private ISpawnerInterface<AsteroidType> m_asteroidSpawner;
        [Inject] private ISpawnerInterface<UFOType> m_ufoSpawner;
        [Inject] private IUIManagerInterface m_UIController;

        private bool m_isGameOver;
        public bool IsGameOver { get => m_isGameOver; }

        public void StartGame()
        {
            m_asteroidSpawner.Start();
            m_ufoSpawner.Start();
            m_UIController.ShowGameScreen();
        }

        public void EndGame()
        {
            m_asteroidSpawner.Stop();
            m_ufoSpawner.Stop();
            m_UIController.ShowEndScreen();
        }

        public void Reload()
        {
            SceneManager.LoadScene(0);
        }
    }
}

