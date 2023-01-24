
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

        void Start()
        {
            force = speedReflectors.force;
            pointvalue = 0;
            pointvalue = speedReflectors.pointValue;
            if (spawnBall) { mechanics = FindObjectOfType<Mechanics>(); }
        }
        public virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
<<<<<<< HEAD
                animator.SetBool("isTouched", true);
=======
                
>>>>>>> parent of 0a59ad7 (scripts violations are cleared now back to graphics)
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
<<<<<<< HEAD
                animator.SetBool("isTouched", false);
=======
                if(animator!=null)
                animator.SetTrigger("isTouched");
>>>>>>> parent of 0a59ad7 (scripts violations are cleared now back to graphics)
            }
        }

    }
}

