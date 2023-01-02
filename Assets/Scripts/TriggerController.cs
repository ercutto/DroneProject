using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PinBall {
    public class TriggerController : MonoBehaviour
    {
        public BallHit ballHit = null;
        public Image SpeedBar;
        public float ballSpeed;
        // Start is called before the first frame update
        void Start()
        {
            SpeedBar.fillAmount = 0.1f;
        }

        // Update is called once per frame
        void Update()
        {
            if (ballHit != null)
            {
                ballSpeed = ballHit.addSpeed;
                SpeedBar.fillAmount = ballSpeed/100;
            }
           
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<BallHit>().thisIsMainBall)
            { ballHit = other.gameObject.GetComponent<BallHit>(); }
 
        }
        public void CollectPower()
        {
            if (ballHit != null) { 
                ballHit.Trigger_KeyDown();
                
            }
            
        }
        public void HitToBall()    
        {
            if (ballHit != null)
            {
                ballHit.Trigger_KeyUp();
            }
                
        }
    }
}

