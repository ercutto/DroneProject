using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class DarkHole : MonoBehaviour
    {
        public Transform toTransform;
        //belongs to ball
        GameObject ball;
        TrailRenderer tRenderer;
        Rigidbody rb;
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ball"))
            {
                ball = other.gameObject;
                animator.SetTrigger("eat");
                tRenderer = ball.GetComponentInChildren<TrailRenderer>();
                rb = ball.GetComponent<Rigidbody>();
                StartCoroutine(TransportBall());

            }
        }
        IEnumerator TransportBall()
        {
            tRenderer.enabled = false;
            ball.transform.position = toTransform.position;
            rb.isKinematic = true;
            yield return new WaitForSeconds(2);
            rb.isKinematic = false;
            tRenderer.enabled = true;

        }
    }
}

