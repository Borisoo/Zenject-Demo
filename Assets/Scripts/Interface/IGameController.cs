
namespace Asteroids
{
public interface IGameController
{
    void StartGame();
    void EndGame();
    bool IsGameOver{get;}
    void Reload();
}
}