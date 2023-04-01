using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    public class PuzzleBoss : MonoBehaviour
    {
        public GameObject ring;
        public GameObject secondRing;
        public GameObject currentObj;
        private Quaternion lookpos;
        public bool isObjectRatated,finished;
        private GameObject obj;
        public GameObject Diamond;

        public int to=0;
        public GameObject[] puzzleObjects;
        public GameObject[] DiamondArms;
        public int opendiamond = 12;
        Vector3 relativerotation;
        private Coroutine LookCoroutine;
        // Start is called before the first frame update
        void Start()
        {
            isObjectRatated = true;
            finished = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (!isObjectRatated)
            {

                Debug.Log(to);
                Debug.Log(finished);

            }
            if (!finished) { return; }
            else
            {
                foreach (var item in DiamondArms)
                {
                    if (item.transform.eulerAngles.x < 45f)
                    { item.transform.eulerAngles = new Vector3(item.transform.eulerAngles.x + 5 * Time.deltaTime, item.transform.eulerAngles.y, 0); }
                    else
                    {
                        Diamond.GetComponent<Rigidbody>().useGravity = true;
                        Diamond.transform.Rotate(transform.up * Time.time * 5f);
                    }


                }
            }
            
        }
        public void StartCoroutine(int to)
        {
            opendiamond -= 1;

            if (opendiamond == 0)
            {
                Debug.Log("oldu");
                finished = true;
                
            }


            if (LookCoroutine != null)
            {
                StopCoroutine(LookCoroutine);
            }
            LookCoroutine =StartCoroutine(LookAt(to));
        }
      
        private IEnumerator LookAt(int to)
        {

            Quaternion LookRotation = Quaternion.LookRotation(puzzleObjects[to].transform.position - secondRing.transform.position);
            float time = 0;
            while (time < 1)
            {
                secondRing.transform.rotation = Quaternion.Slerp(secondRing.transform.rotation, LookRotation, time);
                time += Time.deltaTime * 1f;
                yield return null;
            }
           

        }


    }
}

