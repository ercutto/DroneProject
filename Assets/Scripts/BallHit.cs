using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class BallHit : MonoBehaviour
    {
        /// <summary>
        /// ball hit is controlling allthe ball movements and collision checks
        /// if this is not main ball can not be throw to table back trigger destroys
        /// </summary>
        public GameObject Ground;
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
        private Mechanics mechanics;
        public bool thisIsMainBall;
        public float addSpeed, maxSpeed;
        bool pushing, relased;
        public bool _editing;
        private Reflector reflector = null;
        
        private Keepers keepers = null;
        




        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            Rb = GetComponent<Rigidbody>();
            isOnPull = false;
            isGameStart = false;
            hitToReflector = false;
            addSpeed = 2;
            pushing = false;
            relased = false;
            GameObject.FindGameObjectWithTag("Ground");
            mechanics = FindObjectOfType<Mechanics>();
            Invoke(nameof(StartGame), startTime);
        }
        void StartGame()
        {
            isGameStart = true;
        }

        // Update is called once per frame
        void Update()
        {
           
            Ball_movement();


        }
        void Ball_movement()
        {
            if (!isGameStart)
            {
                return;
            }
            else
            {
                if (_editing)
                {
                    pushing = Input.GetKey(KeyCode.Space);
                    relased = Input.GetKeyUp(KeyCode.Space);
                }

                //if (Input.GetKey(KeyCode.Space) && isOnPull)
                //{
                //    //800
                //    if (addSpeed < maxSpeed)
                //        addSpeed += 10f;

                //}

                //if (Input.GetKeyUp(KeyCode.Space) && isOnPull)
                //{
                //    //800

                //    Rb.AddForce(Vector3.up * addSpeed);
                //    Rb.AddForce(Vector3.forward * addSpeed);
                //    addSpeed = 2;
                //}

                if (pushing && isOnPull)
                {
                    if (addSpeed < maxSpeed)
                        addSpeed += 25f;

                    //if (relased)
                    //{
                    //    Rb.AddForce(Vector3.up * addSpeed);
                    //    Rb.AddForce(Vector3.forward * addSpeed);
                    //    addSpeed = 2;
                    //}
                }
                if (relased)
                {
                    Rb.AddForce(addSpeed * Time.deltaTime * Vector3.up, ForceMode.Impulse);
                    Rb.AddForce(addSpeed * Time.deltaTime * Vector3.forward, ForceMode.Impulse);
                    addSpeed = 2;
                }




                if (hitToReflector)
                {
                    //Rb.AddForce(Vector3.up *currentHitValue);
                    //Rb.AddForce(Vector3.forward *currentHitValue);


                    Rb.AddForce(currentHitValue * Time.deltaTime * -direction, ForceMode.Impulse);

                }


            }
        }
        #region MobileUI_buttons
        public void Trigger_KeyDown()
        {

            if (!isGameStart)
            {
                return;
            }
            else
            {
                pushing = true;

            }
        }
        public void Trigger_KeyUp()
        {
            if (!isGameStart)
            {
                return;
            }
            else
            {

                relased = true;


            }
        }
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("lose"))
            {

                if (thisIsMainBall)
                {
                    gameManager.BallCount(1);
                    mechanics.isMainBallSpawned = false;
                    mechanics.Spawnball_Main();
                }

                gameManager.TotalBallCount(-1);
                Destroy(gameObject);


            }
            else if (other.gameObject.CompareTag("trigger"))
            {
                if (!thisIsMainBall) { gameManager.TotalBallCount(-1); Destroy(gameObject); }

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
                relased = false;
                pushing = false;
            }

        }
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ref"))
            {
                direction = (collision.transform.position - transform.position).normalized;
                hitToReflector = true;
                reflector = collision.gameObject.GetComponent<Reflector>();
                currentHitValue = reflector.force;


            }
            else if (collision.gameObject.CompareTag("keeper"))
            {
                
                
                direction = (collision.transform.position - transform.position).normalized;
                Keepers keepers = collision.gameObject.GetComponentInParent<Keepers>();
                onTarget = keepers.keeperOnTarget;
                if (onTarget) return;
                else
                {
                    hitToReflector = keepers.isPushing;
                    reflector = collision.gameObject.GetComponent<Reflector>();
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
                if (onTarget) { return; } else hitToReflector = keepers.isPushing;
                reflector = collision.gameObject.GetComponent<Reflector>();
                currentHitValue = reflector.force;
      

            }
        }
        public void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("ref") || collision.gameObject.CompareTag("keeper"))
            {
                hitToReflector = false;
                onTarget = false;
                reflector = collision.gameObject.GetComponent<Reflector>();     
                point = reflector.pointvalue;
                gameManager.AddScore(point);
            }
        }

        void KeeperWork()
        {
            //direction = (currentKeeper.transform.position - transform.position).normalized;
            //keepers = currentKeeper.GetComponentInParent<Keepers>();
            //onTarget = keepers.keeperOnTarget;
            //if (onTarget) return;
            //else
            //{
            //    hitToReflector = keepers.isPushing;
            //    reflector = keepers.GetComponent<Reflector>();
            //    currentHitValue = reflector.force;
            //    point = reflector.pointvalue;
            //    gameManager.AddScore(point);

            //}
        }

    }
}
