
using UnityEngine;
namespace PinBall {
    public class AnimWithTrigger : MonoBehaviour
    {
        public  Animator animator;
        public string key;
        public AudioSource source;
        public AudioClip clip;
        public bool isPuzlle;
        public  Collider coll;
        public Boss boss = null;
        
        public BonusComplating bonusComplating = null;
        public void SetBack()
        {
            if (isPuzlle)
            {
                coll.enabled = true;
                if (this.gameObject.activeInHierarchy)
                {
                    //bonusComplating.SetBack();

                    animator.SetTrigger("restart");
                    boss.bossHealth = 0;
                }
                
                
            }
        }
            
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ball"))
            {
                if (isPuzlle) { coll.enabled = false; boss.BossHealthCont(); }

                animator.SetTrigger(key);

                source.PlayOneShot(clip);
            }
           
        }
    }
}

