using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    public class Boss : MonoBehaviour
    {
        private int bossHealth;
        public ChangeCurrents changeCurrents;
        void Start()
        {
            bossHealth = 0;
        }


        void Update()
        {

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                bossHealth+=1;
                Debug.Log("boshealth: " + bossHealth);
                if (bossHealth > 2)
                {
                    bossHealth = 0;
                    changeCurrents.SetBall();
                    changeCurrents.SetBack();
                }
            }
        }
    }
}
