
using UnityEngine;
namespace PinBall
{
    public class BallHit : MonoBehaviour
    {
        /// <summary>
        /// ball hit is controlling allthe ball movements and collision checks
        /// if this is not main ball can not be throw to table back trigger destroys
        /// </summary>
        #region variables
        public GameObject Ground;
        Rigidbody Rb;
        bool isOnPull;
        bool hitToReflector;
        public bool onTarget;
        public float startTime;
        bool isGameStart;
        private float currentHitValue;
        public int point = 0;
        Vector3 direction;
        private GameManager gameManager;
        private Mechanics mechanics;
        public bool thisIsMainBall;
        public float addSpeed, maxSpeed;
        bool pushing, relased;
        public bool _editing;
        private Reflector reflector = null;
        private Keepers keepers = null;
        private string _refTag = "ref";
        private string _kepTag = "keeper";
        private Vector3 from;      
        #endregion
        //private Keepers keepers = null;
        // Start is called before the first frame update
        #region Start&Update
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
            mechanics = GameObject.FindGameObjectWithTag("mech").GetComponent<Mechanics>();
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
        #endregion
        #region ballMovement
        void Ball_movement()
        {
            if (!isGameStart)
            {
                return;
            }
            else
            {
               
                if (pushing && isOnPull)
                {
                    if (addSpeed < maxSpeed)
                        addSpeed += 25f;

                }
                if (relased) { RelasedForce(); }
  

                if (hitToReflector) {AfterHit(); }
             


            }
        }

        void RelasedForce()
        {
            Rb.AddForce(addSpeed * Time.deltaTime * Vector3.up, ForceMode.Impulse);
            Rb.AddForce(addSpeed * Time.deltaTime * Vector3.forward, ForceMode.Impulse);
            addSpeed = 2;
        }
        void AfterHit()
        {
            Rb.AddForce(currentHitValue * Time.deltaTime * -direction, ForceMode.Impulse);
           
        }
        #endregion
        #region MobileUI_buttons
        public void Trigger_KeyDown()
        {

            if (!isGameStart) { return; } else { pushing = true; }
            
        }
        public void Trigger_KeyUp()
        {
            if (!isGameStart) { return; } else { relased = true; }
           
        }
        #endregion
        #region triggers
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

                AfterTrigger();
            }
            else if (other.gameObject.CompareTag("trigger"))
            {
                if (!thisIsMainBall) { AfterTrigger(); }

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
        void AfterTrigger()
        {
            gameManager.TotalBallCount(-1); Destroy(gameObject);
        }
        #endregion
        #region collision
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(_refTag))
            {
                from = collision.transform.position;
                reflector = collision.gameObject.GetComponent<Reflector>();
                currentHitValue = reflector.force;
                hitToReflector = true;
                CallDirection();
                point = reflector.pointvalue;
                gameManager.AddScore(point);

            }
            else if (collision.gameObject.CompareTag(_kepTag))
            {

                from = collision.transform.position;
                
                keepers = collision.gameObject.GetComponentInParent<Keepers>();
                onTarget = keepers.keeperOnTarget;
                if (onTarget) return;
                else
                {
                    hitToReflector = keepers.isPushing;
                    
                    //reflector = collision.gameObject.GetComponent<Reflector>();
                    currentHitValue = keepers.force;
                    CallDirection();



                }
            }
        }
        void CallDirection() {
            direction = (from - transform.position).normalized;
        }
        public void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(_refTag) || collision.gameObject.CompareTag(_kepTag))
            {
                hitToReflector = false;
                onTarget = false;
                //reflector = collision.gameObject.GetComponent<Reflector>();     
                //point = reflector.pointvalue;
                //gameManager.AddScore(point);
                
            }
        }

     

    }
    #endregion
}
