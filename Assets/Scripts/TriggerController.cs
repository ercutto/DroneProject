
using UnityEngine;
using UnityEngine.UI;
namespace PinBall {
    public class TriggerController : MonoBehaviour
    {
        public BallHit ballHit = null;
        public Image SpeedBar;
        public float ballSpeed;
        public float max=500;
        public UIController uIController;
        public TriggerAnimationSytem triggerAnimationSytem;


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
                FillBar();
                
            }
           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<BallHit>().thisIsMainBall)
            {
                uIController.ActiveOrFalse(uIController.TriggerButton);
                ballHit = other.gameObject.GetComponent<BallHit>();
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<BallHit>().thisIsMainBall)
            {
                Invoke(nameof(SetingFalse),0.7f);
                
            }

        }
        public void CollectPower()
        {
            if (ballHit != null) {
              
                ballHit.Trigger_KeyDown();
                triggerAnimationSytem.isPushingStarted = ballHit.pushing;
                
                

            }

        }
        public void HitToBall()    
        {
            if (ballHit != null)
            {
                ballHit.Trigger_KeyUp();
                

            }


        }
        public void FillBar()
        {
            ballSpeed = ballHit.addSpeed;
            SpeedBar.fillAmount = ballSpeed/max;
            
        }
        void SetingFalse() {
            uIController.ActiveOrFalse(uIController.TriggerButton);
             }
        
    }
}

