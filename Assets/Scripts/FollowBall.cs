using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    /// <summary>
    /// this script follow balls at x axis and if ball lose finds new ball
    /// </summary>
    public class FollowBall : MonoBehaviour
    {
        private GameObject ball;
        public CameraController cameraController;
        private bool _followBall;
        float offset;
        Vector3 toMove;
        // Start is called before the first frame update
        void Start()
        {   
            ball = GameObject.FindGameObjectWithTag("ball");
            ;
        }

        // Update is called once per frame
        void Update()
        {
            if (!cameraController.followBall) { return; }
            else { SearchBall(); }
            


        }
        void SearchBall()
        {
            if (ball == null)
            {

                GameObject current = GameObject.Find("mainBall");
                if (current!=null)
                {
                    ball = current;
                }
                else
                {
                   current = GameObject.FindGameObjectWithTag("ball");
                }
            }
            else
            {
                toMove = new Vector3(Mathf.Clamp((ball.transform.position.x + offset), -1.3f, 1.3f), transform.position.y, transform.position.z);

            }
        }
        private void LateUpdate()
        {
            if (ball != null) { transform.position = Vector3.Slerp(transform.position, toMove, 0.5f * Time.deltaTime); }
            
        }
    }
}

