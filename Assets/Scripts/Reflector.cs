
using UnityEngine;
namespace PinBall{
    public class Reflector : MonoBehaviour
    {
        /// <summary>
        /// this can be used for all game object in the table and first you need to addscriptable object
        /// </summary>
        public SpeedReflectors speedReflectors;
        public float force;
        public int pointvalue;
        public  Animator animator;
        public bool spawnBall = false;
        public Mechanics mechanics;
        private AudioClip _clip;
        public AudioSource effectsAudioSource;
        void Start()
        {
            _clip = speedReflectors.clip;
            force = speedReflectors.force;
            pointvalue = 0;
            pointvalue = speedReflectors.pointValue;
            if (spawnBall) { mechanics = FindObjectOfType<Mechanics>(); }
        }
        public void IsTouched()
        {
            if (animator != null) { animator.SetTrigger("eat"); effectsAudioSource.PlayOneShot(_clip); }
                
        }
        //public virtual void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.CompareTag("ball"))
        //    {
        //        if(animator!=null)
        //        animator.SetBool("isTouched", true);
               
        //        if (spawnBall)
        //        {
        //            if (collision.gameObject.GetComponent<BallHit>().thisIsMainBall == true)
        //            {
        //                mechanics.isSpawned = false;
        //                mechanics.Spawnball();
        //            }
                   
        //        }
        //    }
        //}
        //public virtual void OnCollisionExit(Collision collision)
        //{
            
        //  if (collision.gameObject.CompareTag("ball"))
        //    {
        //        if (animator != null)
        //            animator.SetBool("isTouched", false);
        //    }
        //}
      
    }
}

