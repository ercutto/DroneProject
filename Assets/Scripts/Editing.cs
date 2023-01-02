using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    public class Editing : MonoBehaviour
    {
        public bool editing;
        public GameObject[] objects;
        public GameObject ballPrefab;
       
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Mode();


        }
        private void Mode()
        {
            foreach (var item in objects)
            {
                item.GetComponent<Keepers>()._editing = editing;
            }
            ballPrefab.GetComponent<BallHit>()._editing = editing;
        }
    }
}

