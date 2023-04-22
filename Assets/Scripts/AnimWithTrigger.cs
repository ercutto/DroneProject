
using UnityEngine;
namespace PinBall {
    public class AnimWithTrigger : MonoBehaviour
    {
        public  Animator animator;
        public string key;
        public AudioSource source;
        public AudioClip clip;
        public bool isPuzlle;
        private BoxCollider coll=null;
        public Boss boss = null;
        private string Idle = "Idle";
        private string Eat = "Eat";
        public GameManager gameManager;

        public BonusComplating bonusComplating = null;
        public void Start()
        {
            if (isPuzlle) { coll = GetComponent<BoxCollider>(); }
        }
        public void SetBack()
        {
            if (isPuzlle)
            {
               
                if (this.gameObject.activeInHierarchy)
                {
                    coll.enabled = true;
                    //bonusComplating.SetBack();

                    animator.Play(Idle,0);
                    
                }
                
                
            }
        }
            
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ball"))
            {
                if (isPuzlle) { coll.enabled = false; animator.Play(Eat,0); boss.BossHealthCont(); gameManager.AddScore(500); }
                else
                {
                    animator.SetTrigger(key);
                }
                

                source.PlayOneShot(clip);
            }
           
        }
    }
}

