using UnityEngine;
using Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Asteroids
{
public sealed class ScoreManager : IScoreHandler
{   
    private int score;
    public int GetScore{ get=> score; }
    private UnityAction updateScoreAction;
    public  UnityAction UpdateScoreAction { get => updateScoreAction; set => updateScoreAction = value; }
    public void UpdateScore(int score)
    { 
        this.score += score;
        updateScoreAction?.Invoke();
    }
}
}