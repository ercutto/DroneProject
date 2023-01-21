using UnityEngine;
namespace PinBall
{
    public class GroubAnimCont : Reflector
    {
        public SpeedReflectors reflector;
        void Start()
        {
            force = reflector.force;
            animator = GetComponent<Animator>();
        }

        public void Anim()
        {
            animator.SetTrigger("isTouched");
        }

    }

}

