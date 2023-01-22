
using UnityEngine;
namespace PinBall
{
    public class GroubAnimCont : Reflector
    {
        public SpeedReflectors reflector;
        // Start is called before the first frame update
        void Awake()
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

