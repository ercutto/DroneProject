
using UnityEngine;
namespace PinBall {
    public class AnimWithTrigger : MonoBehaviour
    {
        public  Animator animator;
        public string key;
 
        private void OnTriggerEnter(Collider other)
        {
            animator.SetTrigger(key);
        }
    }
}

