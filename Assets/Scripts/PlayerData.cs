using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PinBall
{
    public class PlayerData : MonoBehaviour
    {
        private float score = 0000f;
        private float currentHighScore;
        public Text HighScoreText;
        // Start is called before the first frame update
        void Start()
        {
            currentHighScore = PlayerPrefs.GetFloat("highScore", score);
            TypeToTextArea();
        }
        public void Save(float  score)
        {
            if (currentHighScore >= score)
            { return; }
            else {
                currentHighScore = score;
                PlayerPrefs.SetFloat("highScore", currentHighScore);
                TypeToTextArea();
            }
            
        }
        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("highScore");
        }
        void TypeToTextArea()
        { HighScoreText.text = currentHighScore.ToString(); }
    }

}

