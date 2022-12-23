using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    public class Keepers : MonoBehaviour
    {
        // keepers roatations quaternions if isPushing True keepers turns to keeperTarget else keeper turns to keeperStartPos
        public GameObject keeperTarget, keeperStartPos;
        //checks is player pushing to button 
        public bool isPushing;
        public bool keeperOnTarget;
        public bool canPush;

        public float speed;
        //this keys buttons described with string (keyButton) left is a(small) right is d(small)
        public string keyButton;
        Rigidbody rb;
        //this is Firstchilderen meshcollider
        private MeshCollider mColl;

        // Start is called before the first frame update
        void Start()
        {
            isPushing = false;
            keeperOnTarget = false;

            // angleY = transform.eulerAngles.y;
            rb = GetComponent<Rigidbody>();
            mColl = GetComponentInChildren<MeshCollider>();

        }

        // Update is called once per frame
        void Update()
        {
            isPushing = Input.GetKey(keyButton); //true : false;

            if (isPushing)
            {
                KeeperMove(keeperTarget, 1f);

            }
            else { KeeperMove(keeperStartPos, 0f); }



        }
        void KeeperMove(GameObject moveTo, float bouncines)
        {
            Quaternion lookPos = Quaternion.Slerp(transform.rotation, moveTo.transform.rotation, speed * Time.deltaTime).normalized;
            rb.MoveRotation(lookPos);
            mColl.material.bounciness = bouncines;//setting meshcolliders bounciness to 0 f player is not pushing to button
            keeperOnTarget = rb.transform.rotation == lookPos;//if keeper reachesto limit force is not on use
        }

    }
}

