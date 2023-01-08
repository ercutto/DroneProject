using System.Collections;
using System.Collections.Generic;
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
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Anim()
        {
            animator.SetTrigger("isTouched");
        }

    }

}

