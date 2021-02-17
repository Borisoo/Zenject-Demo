using UnityEngine.Events;

public interface IScoreHandler
{
    void UpdateScore(int score);

    int GetScore{ get; }

    UnityAction UpdateScoreAction{ get;set; }
}