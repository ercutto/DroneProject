
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
        public bool pushing, relased;
        public bool _editing;
        private Reflector reflector = null;
        private Keepers keepers = null;
        //private float speed=1f;
        //private string _refTag = "ref";
        private string _kepTag = "keeper";
        private string _hit = "hit";
        private string _ref = "ref";
        private string _Trigger = "trigger";
        private string _lose = "lose";
        private Vector3 from;
        Vector3 lastVelocity;
        Vector3 velocity=new Vector3(3f,3f,3f);
        public float speedMultiplier = 25f;
        private GameObject currentReflector;
        private int lOrR =2;
        private float count;
       


        //float ySpeed;
        #endregion
        //private Keepers keepers = null;
        // Start is called before the first frame update
        #region Start&Update
        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            gameManager = GameObject.FindObjectOfType<GameManager>();
            isOnPull = false;
            isGameStart = false;
            hitToReflector = false;
            addSpeed = 2;
            pushing = false;
            relased = false;
            count = 0;
            //ySpeed = 0;
            mechanics = GameObject.FindGameObjectWithTag("mech").GetComponent<Mechanics>();
            Invoke(nameof(StartGame), startTime);
        }
        void Start()
        {
           
        }
        void StartGame()
        {
            isGameStart = true;
        }

        // Update is called once per frame
        void Update()
        {

            lastVelocity = Rb.velocity;
            if (Rb.velocity.magnitude < 0.0001f)
            {
                IfBallStuck();

            }
            
            //CheckGround();
            Ball_movement();
           

        }
        void CheckGround()
        {
            //RaycastHit hit;
            //if(Physics.Raycast(transform.position, Vector3.down,out hit, 0.3f))
            //{
            //    if (hit.collider == null) {
            //        ySpeed += Physics.gravity.y * Time.deltaTime;
            //        velocity.y += ySpeed; }

            //}
        
            //if (velocity.magnitude>3f&&!isOnPull)
            //{
            //    Rb.AddForce(lastVelocity * 0.98f*Time.deltaTime);
            //}
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
                        addSpeed += speedMultiplier;
                    
                }
                else { return; }

                if (relased) { RelasedForce();  } 
  

               // if (!hitToReflector) { /*lastVelocity = Rb.velocity;*/ }
               
            }

            


        }

        void RelasedForce()
        {
         
            Rb.AddForce(6 * addSpeed * Time.deltaTime * Vector3.forward, ForceMode.Impulse);
         
            //addSpeed = 2f;
            
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

            if (other.gameObject.CompareTag(_lose))
            {

                if (thisIsMainBall)
                {
                    gameManager.BallCount(1);
                    mechanics.isMainBallSpawned = false;
                    mechanics.Spawnball_Main();
                }

                AfterTrigger();
            }
            else if (other.gameObject.CompareTag(_Trigger))
            {
                if (!thisIsMainBall) { AfterTrigger(); }

            }

        }
        private void OnTriggerStay(Collider other)
        {

            if (other.gameObject.CompareTag(_Trigger))
            {
                isOnPull = true;
            }


        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(_Trigger))
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


            //if (collision.gameObject.CompareTag(_refTag))
            //{
            //    direction = Vector3.Reflect(lastVelocity, collision.GetContact(0).normal);
            //    reflector = collision.gameObject.GetComponent<Reflector>();
            //    AfterCollision();
            //}
            //else
            if (collision.gameObject.CompareTag(_hit))
            {
                currentReflector = collision.gameObject;
                reflector = currentReflector.GetComponent<Reflector>();
                direction = (currentReflector.transform.forward);
                AfterCollisionBumper();
            }
            if (collision.gameObject.CompareTag(_ref))
            {
                currentReflector = collision.gameObject;
                reflector = currentReflector.GetComponent<Reflector>();
                direction = Vector3.Reflect(lastVelocity, collision.GetContact(0).normal);
                AfterCollision();
            }
            else if (collision.gameObject.CompareTag(_kepTag))
            {

                keepers = collision.gameObject.GetComponentInParent<Keepers>();
                onTarget = keepers.keeperOnTarget;
                if (onTarget) return;
                else
                {
                    hitToReflector = keepers.isPushing;
                    currentHitValue = keepers.force;                
                    direction = Vector3.Reflect(lastVelocity, collision.GetContact(0).normal);               
                    //lastVelocity = Vector3.ClampMagnitude(lastVelocity, currentHitValue);

                }
            }

           



        }
        //public void OnCollisionExit(Collision collision)
        //{
        //    if (/*collision.gameObject.CompareTag(_refTag) ||*/ collision.gameObject.CompareTag(_kepTag))
        //    {
        //        hitToReflector = false;


        //    }

        //}

        void AfterCollision()
        {

            currentHitValue = reflector.force;
            point = reflector.pointvalue;
            //hitToReflector = true;
            reflector.IsTouched();
            Rb.AddForce(direction /** currentHitValue*/, ForceMode.Impulse);
            gameManager.AddScore(point);
        }
        void AfterCollisionBumper()
        {

            currentHitValue = reflector.force;
            point = reflector.pointvalue;
            //hitToReflector = true;
            reflector.IsTouched();
            if (direction.magnitude * currentHitValue < velocity.magnitude)
            {
                Rb.AddForce(direction * currentHitValue, ForceMode.Impulse);
            }
            else
            {
                Rb.AddForce(direction * 10f, ForceMode.Impulse);
            }
            
            gameManager.AddScore(point);
        }
        void IfBallStuck()
        {
            if (count < 1)
            {
                count += Time.deltaTime;
                if (count >= 1)
                {
                    lOrR = Random.Range(-lOrR, lOrR);
                    EffectToBall();

                }
                
            }

            
            

           

        }
        void EffectToBall()
        {
            Rb.AddForce(Vector3.right * lOrR, ForceMode.Impulse);
            count = 0;
        }

    }
   
    #endregion
}
