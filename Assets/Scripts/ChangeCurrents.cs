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
        public GameObject gameHealthBar;
        public Animator sceneAnimator;
        public Transform ballToTrigger;
        private string currentAnimation="SceneIdle";
        public AnimatorClipInfo[] animations;
        public bool wait;
        float distance;
        public AudioSource effectSource;
        public AudioClip clip;

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
           
            Invoke(nameof(InvokeAnimatorBoss), 1f);
        }
        public void SetBack()
        {
            gameHealthBar.gameObject.SetActive(true);
            
            animations = sceneAnimator.GetCurrentAnimatorClipInfo(0);
            if(currentAnimation == animations[0].clip.name) { return; }
            {
                Invoke(nameof(InvokeAnimatorMain), 1f);
                first = false;
                second = true;
            }
            

            
            
        }
        void InvokeAnimatorBoss()
        {
            sceneAnimator.SetTrigger("boss");
        }
        void InvokeAnimatorMain()
        {
            sceneAnimator.SetTrigger("main");
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
            effectSource.PlayOneShot(clip);
            transforming = true;
        }
        public void TransformingEnded()
        {
            effectSource.PlayOneShot(clip);
            transforming = false;
        }
        public void SetBall()
        {
           

            ball.transform.position = ballToTrigger.position;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.SetActive(false);
            gateController.GateClosing();
            Invoke(nameof(BallSetBack), 5f);

        }
        void BallSetBack()
        {
            ball.SetActive(true);
        }

      



    }
}
