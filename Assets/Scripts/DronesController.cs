using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DronesActivity
{
    public class DronesController : MonoBehaviour
    {
        
        public float speed = 5;
        private float Fspeed;
        private float Bspeed;
        private float currentRbY;
        private Rigidbody rb;
        public GameObject motorFl, motorRf, motorBl, motorBr;
        public LayerMask ground;

        public bool uP,toFront;

        public Text distanceToGround;
        public Text speedFront;
        public Text speedback;
        public Text xRotation;
        public Text speedawarege;

        private Vector3 startPoint;
        private void Start()
        {
            
            rb = GetComponent<Rigidbody>();
            
            startPoint =new Vector3(0,0,0);
            uP = true;
        }
        private void FixedUpdate()
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f, ground))
            {
                Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, Vector3.down * 10, Color.white);
                Debug.Log("Did not Hit");
            }
           
            //float multiplier = target - hit.distance;
            //speed += multiplier * Time.fixedDeltaTime;
            if(hit.collider != null)
            {

                speed = 128f-hit.distance;
                if (rb.velocity.y < 0)
                { float pullUp = 30f;
                    speed += pullUp - hit.distance;
                }

                
            }
            else
            {
                speed=121.6f;
                
                
            }



            if (uP)
            {
                TuUp(speed);
                currentRbY = rb.velocity.y;
            }
            if (toFront)
            {
               
                if (transform.eulerAngles.x < 2f)
                {
                    uP = false;
                
                    Fspeed = speed + 1f;
                    Bspeed = speed - 1f;
                    TuFront(Fspeed, Bspeed);
                   
  
                }
                else
                {
                    toFront = false;
                    uP = true;
                   
                }
                
            }


            if (Input.GetKey(KeyCode.Space))
            {
                toFront = true;
            }






            distanceToGround.text = hit.distance.ToString("0"+"raycastHits");
            speedFront.text = Fspeed.ToString("00" + " front");
            speedback.text = Bspeed.ToString("00" + " Back");
            xRotation.text = transform.eulerAngles.x.ToString("00" + " rotation");
            speedawarege.text = speed.ToString("00" + " speed");


        }
        void TuUp(float speed)
        {
            LeftFornt(speed);
            RigthFront(speed);
            LeftBack(speed);
            RigthBack(speed);
        }
        void TuFront(float back,float front)
        {
         
            LeftFornt(front);
            RigthFront(front);
            LeftBack(back);
            RigthBack(back);
        }
        void LeftFornt(float speed)
        {
            rb.AddForceAtPosition(motorFl.transform.up * speed * Time.fixedDeltaTime, motorFl.transform.position);
        }
        void RigthFront(float speed)
        {
            rb.AddForceAtPosition(motorRf.transform.up * speed * Time.fixedDeltaTime, motorRf.transform.position);

        }
        void LeftBack(float speed)
        {
            rb.AddForceAtPosition(motorBl.transform.up * speed * Time.fixedDeltaTime, motorBl.transform.position);

        }
        void RigthBack(float speed)
        {
            rb.AddForceAtPosition(motorBr.transform.up * speed * Time.fixedDeltaTime, motorBr.transform.position);

        }

      
    }

      
}

