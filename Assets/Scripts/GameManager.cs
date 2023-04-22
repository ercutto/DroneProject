using System.Collections;

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
        public Mechanics mechanics;
        public Restarter restarter;
        public PlayerData playerData;
        public UIController uIController;
        public ForgeGarbages forgeGarbages;
        public ChangeCurrents changeCurrents;

        public Image ballHitBar;
        public Image BossHealthBar;
        public int maxHit=10;
        public Boss boss;
        public AudioSource effectAudioSource;
        public AudioClip ballMinusSound,ballPlusSound;
        public GoogleAdMobController googleAdMobController;
        public int rewardMaxCount = 3;
        public int rewardCount;
        public bool revardCountFinished;
        public Text rewardCoundText;
       
        #endregion
        #region Private

        [Tooltip("CanNotChange (private)")] [SerializeField] private float currentScore;
        //[Tooltip("CanNotChange (private)")] [SerializeField] private string pts = " PTS";
        [Tooltip("CanNotChange (private)")] [SerializeField] private float bonusTime, bonusTimeEnd;
        private int bonus;
        //[Tooltip("CanNotChange (private)")] [SerializeField] private int countOfHit;
        [Tooltip("CanNotChange (private)")] [SerializeField] private bool countDown;
        private string scoreString, bonusString;
        private int[] Scores = { 1000, 2000,3000,4000,5000,6000,7000,8000,9000,10000 };


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
            //if (Application.isPlaying)//for editing rotates table!
            //{
            //    table.transform.Rotate(-45, 0, 0);
            //}
            //Application.targetFrameRate = 60;
            

        }

        // Update is called once per frame
        void Update()
        {
            if (ballFinished && NoMoreBallOnScene)
            {
                
                if (Input.GetKeyUp(KeyCode.R)) { ResetGameVariables(); }


            }

            if (countDown) { IsItbonusTime(); }
            

        }
        #endregion
        #region BonusTime System And adding bonnus
        void IsItbonusTime()
        {
            //if (countDown)
            //{
                bonusTime += Time.deltaTime;
                if (bonusTime > bonusTimeEnd)
                {
                    AddBonus();


                }

            //}
        }
        void AddBonus()
        {
            bonusTime = 0;
            scoreString = (currentScore += bonus).ToString();
            bonus = 0;
            bonusString = bonus.ToString();
            ChangeScore();
            ChangeBonus();
            //StartCoroutine(nameof(ShakeText), bonusText);
            //ShakeText(machineBonus);
            countDown = false;

        }
        #endregion
        #region ScoreAdding
        //this adds score and also starts bonusTime counts.
        public void AddScore(int addScore)
        {
            AddHit(1);
            if (countDown)
            { /*countOfHit++;*/
                bonus += addScore;
                bonusString = bonus.ToString();
                bonusTime = 0;              
                if(bonus >= Scores[2]) { TypingMessage(2); }
                else if(bonus >= Scores[3]) { TypingMessage(3); }
                else if(bonus >= Scores[4]) { TypingMessage(4); }
                else
                {
                    messageText.text = messageTexts.messages[0];
                }
            }
            else
            {
                countDown = true;
            }

            
            currentScore += addScore;
            scoreString = currentScore.ToString();
            //ToShakeText(machineScore);
            ChangeBonus();
        }
        void TypingMessage(int messages)
        {
            if(messageText.text!= messageTexts.messages[messages])
            {
                messageText.text = messageTexts.messages[messages];
            }
            
        }

        #endregion
        #region texts are On write to text aras
        void ChangeBonus()
        {
            machineBonus.text = bonusString;
        }
        void ChangeScore()
        {
            machineScore.text = scoreString;

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
                machineBallCount.text = currentBall.ToString();
                mechanics.isMainBallSpawned = false;
                currentHit = 0;
                ballHitBar.fillAmount = 0.0f;
                rewardMaxCount = 3;
                rewardCount = 1;
                revardCountFinished = false;
                rewardCoundText.text = rewardMaxCount.ToString();
                boss.AnimatorsReset();
                ChangeScore();
                Invoke(nameof(SpawningBall),3);
                restarter.RestartAll();
                //countOfHit = 0;
            }
            else
            {
                return;
            }

        }
        void SpawningBall()
        {
            mechanics.Spawnball_Main();
        }
        #endregion
        #region hitcount
        public void AddHit(int hit)
        {
            if (!changeCurrents.first)
            {
                if(ballHitBar.gameObject.activeInHierarchy){
                    
                    BossHealthBar.gameObject.SetActive(false);
                }
               

                currentHit += hit;
                
                if (currentHit >= maxHit)
                {
                    
                    currentHit = 0;
                    ballHitBar.fillAmount = (float)currentHit / maxHit;

                    ballHitBar.gameObject.SetActive(false);
                    changeCurrents.SetBall();
                    changeCurrents.ChangeCurrent();
                    
                    
                    return;
                }

                ballHitBar.fillAmount = (float)currentHit / maxHit;
            }
           
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

            if (ballCount == 1) {  effectAudioSource.PlayOneShot(ballMinusSound); }
            else { effectAudioSource.PlayOneShot(ballPlusSound); }

            currentBall -= ballCount;

            if (currentBall <= 0)
            {
                currentBall = 0;

                ballFinished = true;
                

               

            }
          

            ballCountText.text = currentBall.ToString();
            machineBallCount.text = currentBall.ToString();

        }
        public void TotalBallCount(int amount)
        {
            totalAmountOfBall += amount;
            if (totalAmountOfBall < 1)
            {
                WaitforPlayerSelectingContinueOrRestart();
            }

        }
        #endregion
        #region PlayerContinueOrRestart
        public void WaitforPlayerSelectingContinueOrRestart()
        {
            uIController.ActiveOrFalse(uIController.GameOverUI);
        }
        public void PlayerSelectToContinue()
        {
            if (rewardMaxCount>0)
            {
                revardCountFinished = false;
                rewardMaxCount-=rewardCount;
                rewardCoundText.text = rewardMaxCount.ToString();
            }
            else
            {
                revardCountFinished = true;
            }
            

            if (revardCountFinished)
            {
                return;
            }
            else
            {
                uIController.ActiveOrFalse(uIController.GameOverUI);
                ballFinished = false;
                NoMoreBallOnScene = false;
                BallCount(-1);
                mechanics.isMainBallSpawned = false;
                mechanics.Spawnball_Main();
            }
           
        }
        public void WaitforPlayerSelectNotToContiue()
        {
            NoMoreBallOnScene = true;
            playerData.Save(currentScore);
            restarter.RestartAll();
            boss.AnimatorsReset();
            changeCurrents.SetBack();
            
        }
        #endregion
        #region AddMobPlay

        public void PlayAddmobFuncion()
        {
            googleAdMobController.RequestBannerAd();
        }
        public void CallRewardVideo()
        {
            if (revardCountFinished) { return; }
            else {googleAdMobController.RequestAndLoadRewardedAd(); }
        }
        #endregion
    }
}

