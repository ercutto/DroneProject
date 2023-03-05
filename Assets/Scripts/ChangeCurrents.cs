using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class ChangeCurrents : MonoBehaviour
    {
        
        public GameObject pinballObjects;
        public Transform pinBallTo;
        public Transform pinBallfrom;
        private Vector3 pinballStart;
        private Vector3 pinballEnd;

        public GameObject Boss;
        public Transform bossTo;
        public Transform bossFrom;
        private Vector3 bossStart;
        private Vector3 bosEnd;

        public GameObject ball;
        public GameObject colliders;
        public bool first,second;
        

        // Start is called before the first frame update
        void Start()
        {
            if (!ball) { ball = GameObject.FindGameObjectWithTag("ball"); }

            bosEnd = bossTo.transform.position;
            bossStart = bossFrom.transform.position;
            pinballStart = pinBallfrom.transform.position;
            pinballEnd = pinBallTo.transform.position;
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
                colliders.SetActive(false);
                ToMove(Boss,bosEnd);
                ToMove(pinballObjects,pinballEnd);

                Invoke(nameof(SetBack), 10f);

            }

            if (second)
            {

              
                ToMove(Boss,bossStart);
                ToMove(pinballObjects,pinballStart);



            }

        }
        public void ChangeCurrent()
        {
            first = true;
            second = false;
            
        }
        void SetBack()
        {
            
            first = false;
            second = true;
            
        }

        void ToMove(GameObject obj, Vector3 to)
        {
            Vector3 current = obj.transform.position;
            if (current == to) {  return; }else { obj.transform.position = Vector3.Lerp(current, to, 1f * Time.deltaTime); }

            //if (to.y - current.y < 1f) { SetColliderFalse(); }
        }
        void SetColliderFalse()
        {
            
        }
      




    }
}
