
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
        
        public  Animator animator=null;
        public bool spawnBall = false;
        public Mechanics mechanics;

        void Awake()
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
                if(animator!=null)
                animator.SetTrigger("isTouched");
            }
        }

    }
}

