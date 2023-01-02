using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class CameraController : MonoBehaviour
    {
    /// <summary>
        /// this code used for change camera position from one another position.
        /// Player can only change camera position after camera complates movement!
        /// last object of array is followBall object
        /// </summary>
        public GameObject camHolder;
        public GameObject[] camPos;
        public bool camTookPos,followBall;
        public GameObject ball;
    #region privates
        Camera cam;
        Rigidbody camRb;
        Quaternion next;
        Vector3 nextMove;
        Quaternion lookPos;
        Vector3 MovePos;
        int i = 0;
        bool isPushed;
    #endregion
        void Start()
        {
            isPushed = false;
            cam = Camera.main;
            camRb = camHolder.GetComponent<Rigidbody>();
            camTookPos = true;
        }

        void Update()
        {
            
            if (/*Input.GetKeyUp(KeyCode.C)*/isPushed) { NextCamPos(); }
            //keep empty :)
            if (followBall) { FollowBall(); }//Followball has to be before other cam movement because camTookPos bool returns!
            if (camTookPos) { return; } else { if (!followBall) CurrentCamPos(); else { camTookPos = true; } }

            
        
        }
        void NextCamPos()
        {
            ViewChanged();


            if (camTookPos)
            {
                camTookPos = false;
                i++;
                if (i >= camPos.Length) { i = 0; }
                next = camPos[i].transform.rotation;
                nextMove = camPos[i].transform.position;
                cam.orthographic = (i == 1);
                followBall = (i == 3);

            }
            
           
           
        }
        void CurrentCamPos()
        {
            lookPos = Quaternion.Lerp(cam.transform.rotation, next, 5f * Time.deltaTime).normalized;
            MovePos = Vector3.MoveTowards(cam.transform.position, nextMove, 10f * Time.deltaTime);
            camRb.MovePosition(MovePos);
            camRb.MoveRotation(lookPos);
            camTookPos = (camRb.position == nextMove && camRb.rotation == next);
        }
        void FollowBall()
        {
            camRb.transform.position= camPos[3].transform.position;
        }
        public void ChangeView()
        {
            isPushed = true;
        }
        public void ViewChanged()
        {
            isPushed = false;
        }

    }
    
}

