
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
            Mode();
        }

        // Update is called once per frame
        void Update()
        {
            


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

