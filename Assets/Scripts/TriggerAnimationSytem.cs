using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PinBall
{
    public class TriggerAnimationSytem : MonoBehaviour
    {
        public Image spring;
        public Image ball;
        public float ballAnimSpeed;
        Vector3 startPos;
        
        public Image springTop;
        public bool isPushingStarted;
        public bool isReleaseStarted;
        float springY, springX, springZ;
        Rigidbody2D Ballrb;
        public bool isColliding;
        public BoxCollider2D coll;
        public UIController uIController;
        public GameObject triggerButton;
        private Button trButton;
        // Start Dis called before the first frame update,
        void Start()
        {
            trButton = triggerButton.GetComponent<Button>();
            isColliding = false;
            Ballrb = ball.GetComponent<Rigidbody2D>();
            startPos=ball.transform.position;
            isPushingStarted = false;
            springY = spring.transform.localScale.y;
            springX = spring.transform.localScale.x;
            springZ = spring.transform.localScale.z;
        }

        // Update is called once per frame
        void Update()
        {
                
                if (isPushingStarted)
                {

                    Push();

                }


                if (isReleaseStarted)
                {
                    Release();
                }
      
        }
        public void Push()
        {
            
            if (springY < 0.7f) { return; } else { spring.transform.localScale = new Vector3(springX, springY -= 0.5f * Time.deltaTime, springZ); }
        }
        public void Torelase()
        {
            isReleaseStarted = true;
           
        }
        public void Release()
        {
            if (springY >= 1f)
            { return; }
            else
            {
                spring.transform.localScale = new Vector3(springX, springY += 5f * Time.deltaTime, springZ);
                
                

            }
            Ballrb.AddForce(Vector2.up * ballAnimSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("UIBall"))
            {
                triggerButton.SetActive(true);
            }

        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("UIBall"))
            {
                isPushingStarted = false;
                isReleaseStarted = false;
                Invoke(nameof(IsReleaseFalse), 0.5f);

            }
        
        }
        void IsReleaseFalse()
        {
            isReleaseStarted = false;
            triggerButton.SetActive(false);

        }

    }
}

