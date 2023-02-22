using System.Collections;
using UnityEngine;
namespace PinBall
{
    public class AtoB : MonoBehaviour
    {
        public float speed = 0.5f;
        public GameObject moveObjectPos;
        private bool left;
        void Start()
        {
                InvokeRepeating(nameof(Move), 2, 4);

        }
        void Update()
        {
            MoveAction();
            
        }
        void MoveAction() {
            if (left) { moveObjectPos.transform.position += speed * Time.deltaTime * Vector3.right; }
            else { moveObjectPos.transform.position += speed * Time.deltaTime * Vector3.left; }
        }
        private void Move()
        {
            if(left==false)
                left = true;
            else { left = false; }
        }
        

           
    }
}

