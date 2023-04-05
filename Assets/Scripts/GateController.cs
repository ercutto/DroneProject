
using UnityEngine;
namespace PinBall {
    public class GateController : MonoBehaviour
    {
        public Animator animator;
        private string opening="open";
        private string closing="close";
        public AudioSource effectSource;
        public AudioClip SceneChange;
        [ContextMenu(itemName: "FindRiquaredObjects")]
        public void FindRiquaredObjects()
        {
            effectSource = Camera.main.GetComponent<AudioSource>();
            
        }

        public void GateClosing()
        {
            effectSource.PlayOneShot(SceneChange);
            animator.SetTrigger(closing);
        }
        public void GateOpening()
        {
            animator.SetTrigger(opening);
        }
    }
}

