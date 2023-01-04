using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PinBall
{
    public class GameManager : MonoBehaviour
    {
        #region Public
        public static GameManager _GameManager;
        [Tooltip("ScoreText value is: GameManager.currentScore, MultiplierText value is Gamemanager.Bonus")] public Text scoreText, bonusText, ballCountText, machineScore, machineBonus, machineBallCount, messageText;
        public Color red, blue;
        public MessageTexts messageTexts;
        public int ballMax = 3;
        public int currentBall;
        public int totalAmountOfBall;
        public int currentHit;
        public bool ballFinished, NoMoreBallOnScene;
        public GameObject table;//for rotate table on play mode
        #endregion
        #region Private

        [Tooltip("CanNotChange (private)")] [SerializeField] private float currentScore;
        [Tooltip("CanNotChange (private)")] [SerializeField] private string pts = " PTS";
        [Tooltip("CanNotChange (private)")] [SerializeField] private float bonusTime, bonusTimeEnd;
        [Tooltip("CanNotChange (private)")] [SerializeField] private float bonus;
        //[Tooltip("CanNotChange (private)")] [SerializeField] private int countOfHit;
        [Tooltip("CanNotChange (private)")] [SerializeField] private bool countDown;
        private string scoreString, bonusString;
        public Extras extras;
        #endregion
        #region UnityMethods
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
            if (Application.isPlaying)//for editing rotates table!
            {
                table.transform.Rotate(-45, 0, 0);
            }


            ResetGameVariables();

        }

        // Update is called once per frame
        void Update()
        {
            if (ballFinished && NoMoreBallOnScene)
            {
                if (Input.GetKeyUp(KeyCode.R)) { ResetGameVariables(); }


            }

            IsItbonusTime();

        }
        #endregion
        #region BonusTime System And adding bonnus
        void IsItbonusTime()
        {
            if (countDown)
            {
                bonusTime += Time.deltaTime;
                if (bonusTime > bonusTimeEnd)
                {
                    AddBonus();


                }

            }
        }
        void AddBonus()
        {
            bonusTime = 0;
            scoreString = (currentScore += bonus).ToString();
            bonus = 0;
            bonusString = bonus.ToString();
            ChangeScore();
            //StartCoroutine(nameof(ShakeText), bonusText);
            //ShakeText(machineBonus);
            countDown = false;

        }
        #endregion
        #region ScoreAdding
        //this adds score and also starts bonusTime counts.
        public void AddScore(float addScore)
        {

            if (countDown)
            { /*countOfHit++;*/
                bonus += addScore;
                bonusString = bonus.ToString(); bonusTime = 0;
                if (bonus >= 1000)
                {
                    int i = Random.Range(0, messageTexts.messages.Length);
                    messageText.text = messageTexts.messages[i];
                }
                else
                {
                    messageText.text = "work more";
                }
            }
            else
            {
                countDown = true;
            }

            
            currentScore += addScore;
            scoreString = currentScore.ToString();
            //ToShakeText(machineScore);
            ChangeScore();
        }
        #endregion
        #region texts are On write to text aras
        void ChangeScore()
        {
            bonusText.text = bonusString + " +";
            machineBonus.text = bonusString + " +";
            scoreText.text = scoreString + pts;
            machineScore.text = scoreString + pts;

        }
        #endregion
        #region Reset
        public void ResetGameVariables()
        {
            if (totalAmountOfBall == 0)
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
                currentHit = 0;
                extras.Spawnball_Main();
                //countOfHit = 0;
            }
            else
            {
                return;
            }

        }
        #endregion
        #region hitcount
        public void AddHit(int hit)
        {
            currentHit += hit;
            Debug.Log(currentHit);
        }
        #endregion
        #region text Effect
        void ToShakeText(Text text)
        {
            StartCoroutine(nameof(ShakeText), text);
        }
        IEnumerator ShakeText(Text effect)
        {
            RectTransform rt = effect.rectTransform;
            Vector2 startPos = rt.anchoredPosition;

            for (float t = 0; t <= 1; t += Time.deltaTime * 1)
            {
                rt.anchoredPosition = startPos + new Vector2(transform.position.x + t, transform.position.y + t);
                effect.color = blue;

                yield return null;
            }
            for (float t = 0; t <= 1; t += Time.deltaTime * 1)
            {
                rt.anchoredPosition = startPos + new Vector2(transform.position.x * (1 - t), transform.position.y * (1 - t));
                effect.color = red;

                yield return null;
            }
            rt.anchoredPosition = startPos;
        }
        #endregion
        #region Ball Counts and Checks if there are no more ball game stops
        public void BallCount(int ballCount)
        {

            if (currentBall > 0)
            { currentBall -= ballCount; }
            else
            {
                ballFinished = true;
            }

            ballCountText.text = currentBall.ToString();
            machineBallCount.text = currentBall.ToString();

        }
        public void TotalBallCount(int amount)
        {
            totalAmountOfBall += amount;
            if (totalAmountOfBall < 1)
            { NoMoreBallOnScene = true; }



        }
        #endregion
    }
}

