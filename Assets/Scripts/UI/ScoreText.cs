using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace Asteroids
{
    public sealed class ScoreText : MonoBehaviour
    {
        private Text m_scoreText;
        private IScoreHandler m_scoreHandleInterface;

        [Inject]
        public void Construct(IScoreHandler scoreHadler)
        {
            this.m_scoreHandleInterface = scoreHadler;
        }

        private void Start()
        {
            m_scoreText = GetComponent<Text>();
            m_scoreHandleInterface.UpdateScoreAction += UpdateScore;
            m_scoreText.text = "SCORE:0";
        }

        private void UpdateScore()
        {
            var score = m_scoreHandleInterface.GetScore;
            m_scoreText.text = "SCORE:" + score;
        }
    }
}