using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PinBall {
    public class Boss : MonoBehaviour
    {
        public int bossHealth;
        public int maxBosHealth=2;
        public Image bosshealthBar;
        public Image gamehealthBar;
        public ChangeCurrents changeCurrents;
        public GameManager gameManager;
        private string ballTag="ball";
        public BonusComplating bonusComplating;
        public AnimWithTrigger[] animWithTriggers;
        public Restarter restarter;
        void Start()
        {
            AnimatorsReset();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(ballTag))
            {
                if (!bosshealthBar.gameObject.activeInHierarchy)
                {

                    bosshealthBar.gameObject.SetActive(true);
                }

               
            }
        }
        public void BossHealthCont()
        {
            if (!bosshealthBar.gameObject.activeInHierarchy)
            {

                bosshealthBar.gameObject.SetActive(true);
            }

            bossHealth += 1;
            bosshealthBar.fillAmount = (float)bossHealth / maxBosHealth;

            if (bossHealth >= maxBosHealth)
            {
                bosshealthBar.gameObject.SetActive(false);
                gamehealthBar.gameObject.SetActive(true);
                gameManager.AddScore(10000);
                gameManager.BallCount(-1);
                changeCurrents.SetBall();
                changeCurrents.SetBack();
                restarter.RestartAll();
                Invoke(nameof(AnimatorsReset),2f);


            }
        }
        public void AnimatorsReset()
        {
            bossHealth = 0;
            bosshealthBar.fillAmount = 0.0f;

            foreach (var ToReset in animWithTriggers)
            {
                ToReset.SetBack();

            }
        }
    }
}
