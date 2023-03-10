using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PinBall {
    public class Boss : MonoBehaviour
    {
        private int bossHealth;
        public int maxBosHealth=2;
        public Image bosshealthBar;
        public Image gamehealthBar;
        public ChangeCurrents changeCurrents;
        public GameManager gameManager;
        void Start()
        {
            bossHealth = 0;
            bosshealthBar.fillAmount = 0.0f;
        }


        void Update()
        {

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                if (!bosshealthBar.gameObject.activeInHierarchy)
                {

                    bosshealthBar.gameObject.SetActive(true);
                }

                bossHealth += 1;
                bosshealthBar.fillAmount = (float)bossHealth / maxBosHealth;
                Debug.Log("boshealth: " + bossHealth);
                if (bossHealth >= maxBosHealth)
                {
                    bosshealthBar.gameObject.SetActive(false);
                    gamehealthBar.gameObject.SetActive(true);
                    gameManager.BallCount(-1);
                    bossHealth = 0;
                    changeCurrents.SetBall();
                    changeCurrents.SetBack();
                }
            }
        }
    }
}
