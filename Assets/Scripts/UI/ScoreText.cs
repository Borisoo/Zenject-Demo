using UnityEngine;
using Zenject;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine.UI;

namespace Asteroids
{
public class ScoreText: MonoBehaviour 
{   
    Text scoreText;
    IScoreHandler scoreHandlerInterface;

    [Inject]
    public void Construct(IScoreHandler scoreHadler)
    {
        this.scoreHandlerInterface = scoreHadler;
    }

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreHandlerInterface.UpdateScoreAction += UpdateScore;
        scoreText.text = "SCORE:0";
    }

    void UpdateScore()
    {
        var score = scoreHandlerInterface.GetScore;
        scoreText.text = "SCORE:" + score;
    }
}
}