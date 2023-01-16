using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    public class Restarter : MonoBehaviour
    {
        public GameObject[] toRestart;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void RestartAll()
        {
            foreach (var item in toRestart)
            {
                item.GetComponent<GroubPointsCollect>().ObjectsBactToStart();
            }
        }
    }
}

