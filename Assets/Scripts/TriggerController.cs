
using UnityEngine;
using UnityEngine.UI;
namespace PinBall {
    public class TriggerController : MonoBehaviour
    {
        public BallHit ballHit = null;
        public Image SpeedBar;
        public float ballSpeed;
        public UIController uIController;
        private GameObject triggerButton;
       
        
        // Start is called before the first frame update
        void Start()
        {
            SpeedBar.fillAmount = 0.1f;
            triggerButton = uIController.TriggerButton;
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
                uIController.ActiveOrFalse(triggerButton);
                ballHit = other.gameObject.GetComponent<BallHit>();
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<BallHit>().thisIsMainBall)
            {
                uIController.ActiveOrFalse(triggerButton);
                
            }

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
        public void FillBar()
        {
            ballSpeed = ballHit.addSpeed;
            float max = ballSpeed / 100;
            SpeedBar.fillAmount = Mathf.Lerp(SpeedBar.fillAmount, max, Time.deltaTime);
        }
    }
}

