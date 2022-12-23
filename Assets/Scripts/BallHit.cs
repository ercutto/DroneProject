using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall{
    public class BallHit : MonoBehaviour
    {
        /// <summary>
        /// ball hit is controlling allthe ball movements and collision checks
        /// if this is not main ball can not be throw to table back trigger destroys
        /// </summary>

        Rigidbody Rb;
        bool isOnPull;
        bool hitToReflector;
        public bool onTarget;
        public float startTime;
        bool isGameStart;
        private float currentHitValue;
        public float point = 0;
        Vector3 direction;
        private GameManager gameManager;
        private Extras extras;
        public bool thisIsMainBall;
   
        



        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            Rb = GetComponent<Rigidbody>();
            isOnPull = false;
            isGameStart = false;
            hitToReflector = false;
            extras = FindObjectOfType<Extras>();
            Invoke(nameof(StartGame), startTime);
        }
        void StartGame()
        {
            isGameStart = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isGameStart)
            {
                return;
            }
            else
            {


                if (Input.GetKeyUp(KeyCode.Space) && isOnPull)
                {

                    Rb.AddForce(Vector3.up * 800);
                    Rb.AddForce(Vector3.forward * 800);

                }

                if (hitToReflector)
                {
                    //Rb.AddForce(Vector3.up *currentHitValue);
                    //Rb.AddForce(Vector3.forward *currentHitValue);


                    Rb.AddForce(currentHitValue * Time.deltaTime * -direction, ForceMode.Impulse);

                }


            }


        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("lose"))
            {
               
                if (thisIsMainBall)
                {
                    gameManager.BallCount(1);
                    extras.isMainBallSpawned = false;
                    extras.Spawnball_Main();
                }

                gameManager.TotalBallCount(-1);
                Destroy(gameObject);
                
               
            }
            else if (other.gameObject.CompareTag("trigger")){
                if (!thisIsMainBall) { gameManager.TotalBallCount(-1);Destroy(gameObject); }

            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("trigger"))
            {
                isOnPull = true;
            }
            

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("trigger"))
            {
                isOnPull = false;
            }
        }
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ref"))
            {
                direction = (collision.transform.position - transform.position).normalized;
                hitToReflector = true;
                Reflector reflector = collision.gameObject.GetComponent<Reflector>();
                currentHitValue = reflector.force;
                //point += reflector.pointvalue;
                //gameManager.AddScore(point);

            }
            else if (collision.gameObject.CompareTag("keeper"))
            {
                direction = (collision.transform.position - transform.position).normalized;
                Keepers keepers = collision.gameObject.GetComponentInParent<Keepers>();
                onTarget = keepers.keeperOnTarget;
                if (!onTarget)
                {
                    hitToReflector = keepers.isPushing;
                    Reflector reflector = collision.gameObject.GetComponent<Reflector>();
                    currentHitValue = reflector.force;
                    point = reflector.pointvalue;
                    gameManager.AddScore(point);
                    
                }

            }
        }
        public void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("keeper"))
            {
                direction = (collision.transform.position - transform.position).normalized;
                Keepers keepers = collision.gameObject.GetComponentInParent<Keepers>();
                onTarget = keepers.keeperOnTarget;
                if (!onTarget)
                {
                    hitToReflector = keepers.isPushing;
                    Reflector reflector = collision.gameObject.GetComponent<Reflector>();
                    currentHitValue = reflector.force;
                   
                    
                }

            }
        }
        public void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("ref") || collision.gameObject.CompareTag("keeper"))
            {
                hitToReflector = false;
                onTarget = false;
                Reflector reflector = collision.gameObject.GetComponent<Reflector>();
                point = reflector.pointvalue;
                gameManager.AddScore(point);
            }
        }

       

    }
}