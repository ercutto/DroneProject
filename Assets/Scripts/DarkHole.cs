using System.Collections;

using UnityEngine;
namespace PinBall
{
    public class DarkHole : MonoBehaviour
    {
        public int scoreValue = 450;
        public Transform toTransform;
        //belongs to ball
        GameObject ball;
        TrailRenderer tRenderer;
        Rigidbody rb;
        public Animator animator;
        public bool isDarkHole = true;
        public float WaitingTime = 2;
        public GameManager gameManager;
        public Transform from;
        Vector3 to,_from,ballPos;
        Collider darkCollCollider;
        public AudioSource effects;
        public AudioClip clip,roboEat,PowerUp;
        WaitForSeconds delayIt = new WaitForSeconds(0);
        public Vector3 Hummerdir;
        public ChangeCurrents currents;
        public Transform mainSpawnPoint;
        public Vector3 toMainSpawnPosition;
        
        // Start is called before the first frame update
        void Start()
        {
            delayIt = new WaitForSeconds(WaitingTime);
            Hummerdir = transform.localPosition;
            darkCollCollider = GetComponent<Collider>();
            to = toTransform.position;
            _from = from.transform.position;
            toMainSpawnPosition = mainSpawnPoint.position;
            if (animator == null) { animator = GetComponentInChildren<Animator>(); }
            
            gameManager = FindObjectOfType<GameManager>();
        }

        // Update is called once per frame
     
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ball"))
            {
                darkCollCollider.enabled = false;
                gameManager.AddScore(scoreValue);
                ball = other.gameObject;
                
                tRenderer = ball.GetComponentInChildren<TrailRenderer>();
                rb = ball.GetComponent<Rigidbody>();
                StartCoroutine(TransportBall());

            }
        }
        IEnumerator TransportBall()
        {
            SetComponents();
            yield return delayIt;
            UnSetComponents();

        }
        public virtual void SetComponents()
        {
            
            
           
            if (isDarkHole)
            {
               
                tRenderer.enabled = false;
                if (currents.transforming)
                {
                    ball.transform.position = toMainSpawnPosition;
                }
                else
                {
                    ball.transform.position = to;
                    effects.PlayOneShot(roboEat);

                }
                
            }
            else
            {
                if (currents.transforming)
                {
                    ball.transform.position = toMainSpawnPosition;
                }
                else
                {
                    ball.transform.position = _from;

                    effects.PlayOneShot(PowerUp);
                }
                
            }

            
            animator.SetTrigger("eat");
            
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative; 
          
            rb.isKinematic = true;
            

        }
        public virtual void UnSetComponents()
        {
            
            if (isDarkHole) {
                tRenderer.enabled = true;
                darkCollCollider.enabled = true;

            }
            else
            {
                rb.isKinematic = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                //ball.transform.position = to;
                KickBall();
            }

            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            
            effects.PlayOneShot(clip);
            

        }
        void KickBall()
        {
            
           
             rb.AddForce(transform.right* 80f, ForceMode.Impulse);
             
            Invoke(nameof(ColliderFalse), 2f);
        }
        void ColliderFalse()
        {
            darkCollCollider.enabled = true;
        }
    }
}

