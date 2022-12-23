using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PinBall {
    public class GameManager : MonoBehaviour
    {
        #region Public
        public static GameManager _GameManager;
        [Tooltip("ScoreText value is: GameManager.currentScore, MultiplierText value is Gamemanager.Bonus")] public Text scoreText, bonusText,ballCountText;
        public Color red, blue;
        public int ballMax=3;
        public int currentBall;
        public int totalAmountOfBall;
        public bool ballFinished,NoMoreBallOnScene;
        #endregion
        #region Private

        [Tooltip("CanNotChange (private)")] [SerializeField] private float currentScore;
        [Tooltip("CanNotChange (private)")] [SerializeField] private string pts = " PTS";
        [Tooltip("CanNotChange (private)")] [SerializeField] private float bonusTime, bonusTimeEnd;
        [Tooltip("CanNotChange (private)")] [SerializeField] private float bonus;
        //[Tooltip("CanNotChange (private)")] [SerializeField] private int countOfHit;
        [Tooltip("CanNotChange (private)")] [SerializeField] private bool countDown;
        public Extras extras;
        #endregion
        private void Awake()
        {
            if (_GameManager == null)
            {
                _GameManager = this;
                DontDestroyOnLoad(gameObject);
            }
            else { Destroy(gameObject); }

        }
        // Start is called before the first frame update
        void Start()
        {

            ResetGameVariables();

        }

        // Update is called once per frame
        void Update()
        {
            if (ballFinished && NoMoreBallOnScene) {
                if (Input.GetKeyUp(KeyCode.R)) { ResetGameVariables(); }

                
            }

            IsItbonusTime();
            ChangeScore();
        }
        void IsItbonusTime()
        {
            if (countDown)
            {
                bonusTime += Time.deltaTime;
                if (bonusTime > bonusTimeEnd)
                {
                    bonusTime = 0;
                    currentScore += bonus;
                    StartCoroutine(nameof(ShakeText), bonusText);

                    bonus = 0;
                    //countOfHit = 0;
                    countDown = false;
                }

            }
        }
        //this adds score and also starts bonusTime counts.
        public void AddScore(float addScore)
        {

            if (countDown) { /*countOfHit++;*/ bonus += addScore; bonusTime = 0; } else { countDown = true; }

            ToShakeText();
            currentScore += addScore;


        }
        void ChangeScore()
        {
            bonusText.text = bonus.ToString() + " +";
            scoreText.text = currentScore.ToString() + pts;
        }
        #region Reset
        void ResetGameVariables()
        {

            //totalAmountOfBall = 1;
            ballFinished = false;
            NoMoreBallOnScene = false;
            scoreText.text = "0000 ";
            bonusTime = 0;
            countDown = false;
            bonusTimeEnd = 3f;
            currentScore = 0;
            currentBall = ballMax;
            ballCountText.text = currentBall.ToString();
            extras.isMainBallSpawned = false;
            extras.Spawnball_Main();
            //countOfHit = 0;
        }
        #endregion
        #region text Effect
        void ToShakeText()
        {
            StartCoroutine(nameof(ShakeText), scoreText);
        }
        IEnumerator ShakeText(Text effect)
        {
            RectTransform rt = effect.rectTransform;
            Vector2 startPos = rt.anchoredPosition;

            for (float t = 0; t <= 1; t += Time.deltaTime * 10)
            {
                rt.anchoredPosition = startPos + new Vector2(transform.position.x + t, transform.position.y + t);
                effect.color = blue;

                yield return null;
            }
            for (float t = 0; t <= 1; t += Time.deltaTime * 10)
            {
                rt.anchoredPosition = startPos + new Vector2(transform.position.x * (1 - t), transform.position.y * (1 - t));
                effect.color = red;

                yield return null;
            }
            rt.anchoredPosition = startPos;
        }
        #endregion
        #region BallSystem
        public void BallCount(int ballCount)
        {
           
            if (currentBall > 0)
            { currentBall -= ballCount; }
            else
            {
                ballFinished = true;
            }

            ballCountText.text = currentBall.ToString();

        }
        public void TotalBallCount(int amount)
        {
            totalAmountOfBall += amount;
            if (totalAmountOfBall< 1)
            { NoMoreBallOnScene = true; }
           


        }
        #endregion
    }
}

