using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    public class CollCheck : MonoBehaviour
    {
     
        private Material mat;
        private Collider col;
        private GameManager gameManager;
        public bool isTouched;
     
        private void Start()
        {
            isTouched = false;
            gameManager = FindObjectOfType<GameManager>();
            col = GetComponent<BoxCollider>();
            mat= GetComponent<MeshRenderer>().material;
            

        }
        public void Update()
        {

        }
        public virtual void ChangeColor()
        {
           
                Change(Color.red);
                StartCoroutine(ColorIsWaiting());
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ball")) {
                
                ScoreAndAction();
                
            }

        }
  
        public virtual void ScoreAndAction()
        {
            col.enabled = false;
            ChangeColor();
            gameManager.AddHit(1);
        }
      
        IEnumerator ColorIsWaiting()
        {
            yield return new WaitForSeconds(1);
            Change(Color.white);
            ColliderControll(true);

        }
        
        public virtual void Change(Color color)
        {
            if (mat) { mat.color = color; }
                
           
        }
        public virtual void ColliderControll(bool value)
        {
            col.enabled = value;
        }

       
    }

}


