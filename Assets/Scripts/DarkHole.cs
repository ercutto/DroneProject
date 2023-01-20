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
        Animator animator;
        public bool isDarkHole=true;
        public int WaitingTime=2;
        public GameManager gameManager;
        Collider darkCollCollider;

        // Start is called before the first frame update
        void Start()
        {
            darkCollCollider = GetComponent<Collider>();
            animator = GetComponentInChildren<Animator>();
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
            yield return new WaitForSeconds(WaitingTime);
            UnSetComponents();

        }
        public virtual void SetComponents()
        {
            
            
           
            if (isDarkHole)
            {
               
                tRenderer.enabled = false;
                ball.transform.position = toTransform.position;
            }
            else
            {
                ball.transform.position = this.transform.position;
            }

            
            animator.SetTrigger("eat");
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = true;
            

        }
        public virtual void UnSetComponents()
        {
            
            if (isDarkHole) {
                tRenderer.enabled = true;
            }
            else
            {
                ball.transform.position = toTransform.position;

            }

            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            darkCollCollider.enabled = true;


        }
    }
}

