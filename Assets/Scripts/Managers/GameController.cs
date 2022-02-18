
using Zenject;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Asteroids
{
    public class GameController : IGameController
    {
        [Inject] private ISpawnerInterface<AsteroidType> asteroidSpawner;
        [Inject] private ISpawnerInterface<UFOType> ufoSpawner;
        [Inject] private IUIManagerInterface UIController;

        private bool isGameOver;

        public void StartGame()
        {
            asteroidSpawner.Start();
            ufoSpawner.Start();
            UIController.ShowGameScreen();
        }

        public void EndGame()
        {
            asteroidSpawner.Stop();
            ufoSpawner.Stop();
            UIController.ShowEndScreen();
        }

        public bool IsGameOver { get => isGameOver; }
        public void Reload()
        {
            SceneManager.LoadScene(0);
        }
    }
}

