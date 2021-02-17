
using Zenject;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Asteroids
{
    public class GameController : IGameController
    {
        [Inject]
        ISpawnerInterface<AsteroidType> asteroidSpawner;

        [Inject]
        ISpawnerInterface<UFOType> ufoSpawner;  

        [Inject]
        IUIManagerInterface UIController;
        

        bool isGameOver;
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

        public bool IsGameOver{get => isGameOver; }
        public void Reload()
        {
          SceneManager.LoadScene(0);
        }
    }
}

