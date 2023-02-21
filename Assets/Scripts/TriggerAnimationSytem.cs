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
        public UIController uIController;
        // Start Dis called before the first frame update
        void Start()
        {
            Ballrb = ball.GetComponent<Rigidbody2D>();
            startPos=ball.transform.position;
            isPushingStarted = true;
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
            else if (isReleaseStarted)
            {
                Release();
            }
        }
        void Push()
        {
            
            if (springY < 0.7f) { return; } else { spring.transform.localScale = new Vector3(springX, springY -= 0.1f * Time.deltaTime, springZ); }
        }
        void Release()
        {
            if (springY >= 1f)
            { return; }
            else
            {
                spring.transform.localScale = new Vector3(springX, springY += 3f * Time.deltaTime, springZ);
                Ballrb.AddForce(Vector2.up * ballAnimSpeed * Time.deltaTime, ForceMode2D.Impulse);
              
                
            }
        }
      
    }
}

