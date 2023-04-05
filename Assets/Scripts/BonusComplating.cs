using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    public class BonusComplating : MonoBehaviour
    {

        private Animator animator;
       
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

        }
        public void Count()
        {
            
            animator.SetTrigger("eat");

        }
       
    }
}

