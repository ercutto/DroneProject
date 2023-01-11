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
        public Mechanics mechanics;

        // Start is called before the first frame update
        void Start()
        {
            force = speedReflectors.force;
            pointvalue = 0;
            pointvalue = speedReflectors.pointValue;
            if (spawnBall) { mechanics = FindObjectOfType<Mechanics>(); }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                animator.SetBool("isTouched", true);
                if (spawnBall)
                {
                    if (collision.gameObject.GetComponent<BallHit>().thisIsMainBall == true)
                    {
                        mechanics.isSpawned = false;
                        mechanics.Spawnball();
                    }
                   
                }
            }
        }
        public virtual void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                animator.SetBool("isTouched", false);
            }
        }

    }
}

