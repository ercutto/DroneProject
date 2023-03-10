
using UnityEngine;
namespace PinBall
{
    public class GroubAnimCont : Reflector
    {
        public SpeedReflectors reflector;
        public AudioClip grClip;
        public AudioSource effects;
        
        // Start is called before the first frame update
        void Start()
        {
            
            force = reflector.force;
            
        }

        public override void IsTouched()
        {
            animator.SetTrigger("eat");
            effects.PlayOneShot(grClip);
        }

    }

}

