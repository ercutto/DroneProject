
using UnityEngine;
namespace PinBall
{
    public class GroubAnimCont : Reflector
    {
        public SpeedReflectors reflector;
        private AudioClip grClip;
        public AudioSource effects;
        
        // Start is called before the first frame update
        void Start()
        {
            grClip = reflector.clip;
            force = reflector.force;

        }

        public void Anim()
        {
            animator.SetTrigger("eat");
            effects.PlayOneShot(grClip);
        }

    }

}

