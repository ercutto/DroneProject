
using UnityEngine;
namespace PinBall
{
    public class GroubAnimCont : Reflector
    {
        public SpeedReflectors reflector;
        
        // Start is called before the first frame update
        void Start()
        {
            force = reflector.force;
            
            
        }

        public void Anim()
        {
            animator.SetTrigger("isTouched");
        }

    }

}

