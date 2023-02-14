
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
        Vector3 lastVelocity;
        Vector3 velocity;

        float ySpeed;
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
            ySpeed = 0;
            mechanics = GameObject.FindGameObjectWithTag("mech").GetComponent<Mechanics>();
            Invoke(nameof(StartGame), startTime);
        }
        void StartGame()
        {
            isGameStart = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            

            CheckGround();

            Ball_movement();
           

        }
        void CheckGround()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down,out hit, 0.3f))
            {
                if (hit.collider == null) {
                    ySpeed += Physics.gravity.y * Time.deltaTime;
                    velocity.y += ySpeed; }
                
            }
            if (velocity.magnitude >1f&&!isOnPull)
            {
                Rb.AddForce(lastVelocity * 10f);
            }
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
  

                if (hitToReflector) {AfterHit(); } else { lastVelocity = Rb.velocity; }
             


            }
        }

        void RelasedForce()
        {
            //Rb.AddForce(3 * addSpeed * Time.deltaTime * Vector3.up, ForceMode.Impulse);
            Rb.AddForce(6 * addSpeed * Time.deltaTime * Vector3.forward, ForceMode.Impulse);
            ////Rb.velocity = Vector3.forward.normalized * addSpeed;
            addSpeed = 2f;
            
        }
        void AfterHit()
        {
            // Rb.AddForce(currentHitValue * Time.deltaTime * direction, ForceMode.Impulse);
            
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
              
                reflector = collision.gameObject.GetComponent<Reflector>();
                
                currentHitValue = reflector.force;
                //CallDirection();
                direction = Vector3.Reflect(lastVelocity, collision.GetContact(0).normal);
                hitToReflector = true;
                //Rb.velocity = direction * Mathf.Max(currentHitValue);
                //lastVelocity = Vector3.ClampMagnitude(Rb.velocity,currentHitValue);
                Rb.AddForce(direction * currentHitValue, ForceMode.Impulse);
                point = reflector.pointvalue;
                // Rb.velocity = direction * currentHitValue;//Mathf.Max(currentHitValue);
               // Rb.velocity = direction * (currentHitValue / 2);
                //Rb.velocity = direction * currentHitValue;

                //Rb.velocity =  Mathf.Max(currentHitValue) ;
                gameManager.AddScore(point);

            }
            else if (collision.gameObject.CompareTag("hit"))
            {
                reflector = collision.gameObject.GetComponent<Reflector>();
                
                currentHitValue = reflector.force;
                direction = (collision.gameObject.transform.forward);
                Rb.AddForce(direction * currentHitValue, ForceMode.Impulse);
            }
            else if (collision.gameObject.CompareTag(_kepTag))
            {

                keepers = collision.gameObject.GetComponentInParent<Keepers>();
                onTarget = keepers.keeperOnTarget;
                if (onTarget) return;
                else
                {
                    hitToReflector = keepers.isPushing;

                    //reflector = collision.gameObject.GetComponent<Reflector>();
                    currentHitValue = keepers.force;
                    //CallDirection();
                    direction = Vector3.Reflect(lastVelocity, collision.GetContact(0).normal);
                   // Rb.velocity = direction * Mathf.Max(currentHitValue);
                    lastVelocity = Vector3.ClampMagnitude(lastVelocity, currentHitValue);

                }
            }

           



        }
        //void CallDirection()
        //{
        //    direction = Vector3.Reflect(Rb.velocity.normalized);
        //}

        public void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(_refTag) || collision.gameObject.CompareTag(_kepTag))
            {
                hitToReflector = false;
                //onTarget = false;
                //reflector = collision.gameObject.GetComponent<Reflector>();     
                //point = reflector.pointvalue;
                //gameManager.AddScore(point);
                
            }


        }




}
    #endregion
}
