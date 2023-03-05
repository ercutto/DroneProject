
using UnityEngine;
namespace PinBall {
    public class AnimWithTrigger : MonoBehaviour
    {
        public  Animator animator;
        public string key;
        public AudioSource source;
        public AudioClip clip;
 
        private void OnTriggerEnter(Collider other)
        {
            animator.SetTrigger(key);
            source.PlayOneShot(clip);
        }
    }
}

