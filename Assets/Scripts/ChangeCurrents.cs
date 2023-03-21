using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class ChangeCurrents : MonoBehaviour
    {
        
  

        public GameObject Boss;
        public Transform bossTo;
        public Transform bossFrom;
        private Vector3 bossStart;
        private Vector3 bosEnd;

        public GameObject BossMap;        
        public GameObject ball;
        public GameObject colliders;
        public bool first,second,transforming;
        public GateController gateController;

        public Animator sceneAnimator;
        public Transform ballToTrigger;

        public bool wait;
        float distance;

        // Start is called before the first frame update
        void Start()
        {
            if (!ball) { ball = GameObject.FindGameObjectWithTag("ball"); }
            transforming = false;
            wait=false;
            bosEnd = bossTo.transform.position;
            bossStart = bossFrom.transform.position;
           
        }

        // Update is called once per frame
        void Update()
        {
            if (!ball)
            {
                ball = GameObject.FindGameObjectWithTag("ball");
            }
           

            if (first)
            {
               
                //colliders.SetActive(false);
                ToMove(Boss,bosEnd,true,false);
                //ToMove(pinballObjects,pinballEnd,false,false);
                //ToMove(BossMap,pinballStart,true,false);
                //ball.transform.position = ballToTrigger.position;
                

            }

            if (second)
            {


                //ball.SetActive(false);
                ToMove(Boss,bossStart,false,true);
                //ToMove(pinballObjects,pinballStart,true,true);
                //ToMove(BossMap,pinballEnd, false,true);
               



            }

           


        }
        public void ChangeCurrent()
        {
            first = true;
            second = false;
            sceneAnimator.SetTrigger("boss");
        }
        public void SetBack()
        {
            sceneAnimator.SetTrigger("main");
            first = false;
            second = true;
            
        }

        void ToMove(GameObject obj, Vector3 to,bool activeOrFalse,bool collActiveOrfalse)
        {
            Vector3 current = obj.transform.position;
            if (current == to) {   return; }else { obj.transform.position = Vector3.Lerp(current, to, 1f * Time.deltaTime); }

            //if (to.y - current.y < 1f) { SetColliderFalse(); }
            distance = Vector3.Distance(obj.transform.position, to);
            if (distance< 0.2f)
            {
              
                obj.SetActive(activeOrFalse);
                //colliders.SetActive(activeOrFalse);
                
                
                
            }
            else
            {
                //transforming = false;
            }
            
        }
        public void Transforming()
        {
            transforming = true;
        }
        public void TransformingEnded()
        {
            transforming = false;
        }
        public void SetBall()
        {
           

            ball.transform.position = ballToTrigger.position;
            ball.SetActive(false);
            gateController.GateClosing();
            Invoke(nameof(BallSetBack), 3);

        }
        void BallSetBack()
        {
           
            ball.SetActive(true);
        }

      



    }
}
