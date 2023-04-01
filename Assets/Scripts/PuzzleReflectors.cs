using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class PuzzleReflectors : MonoBehaviour
    {
        SpeedReflectors reflectors;
        float force;
        int point;
        AudioClip audio;
        public PuzzleBoss puzzleBoss;
        private BoxCollider coll;
        public int objNumber;
        
        void Start()
        {
            force = reflectors.force;
            point = reflectors.pointValue;
            audio = reflectors.clip;


            coll = gameObject.GetComponent<BoxCollider>();
           
        }

        // Update is called once per frame
        void Update()
        {

         
            
        }
        private void OnCollisionEnter(Collision collObj)
        {
            if (collObj.gameObject.CompareTag("ball"))
            {
                coll.enabled=false;
                //puzzleBoss.to=objNumber;
                puzzleBoss.StartCoroutine(objNumber);
            }
        }
       


    }
}

