using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall{
    public class Reflector : MonoBehaviour
    {
        /// <summary>
        /// this can be used for all game object in the table and first you need to addscriptable object
        /// </summary>
        public SpeedReflectors speedReflectors;
        public float force;
        public float pointvalue;
        public  Animator animator;
        public bool spawnBall = false;
        public Extras extras;

        // Start is called before the first frame update
        void Start()
        {
            force = speedReflectors.force;
            pointvalue = speedReflectors.pointValue;
            if (spawnBall) { extras = FindObjectOfType<Extras>(); }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                animator.SetBool("isTouched", true);
                if (spawnBall)
                {
                    if (collision.gameObject.GetComponent<BallHit>().thisIsMainBall == true)
                    {
                        extras.isSpawned = false;
                        extras.Spawnball();
                    }
                   
                }
            }
        }
        public void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                animator.SetBool("isTouched", false);
            }
        }

    }
}
