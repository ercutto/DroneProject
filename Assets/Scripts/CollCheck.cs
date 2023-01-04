using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall {
    public class CollCheck : MonoBehaviour
    {
     
        private Material mat;
        private Collider col;
        private GameManager gameManager;
        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            col = GetComponent<BoxCollider>();
            mat= GetComponent<MeshRenderer>().material;

        }
        private void Update()
        {
            
        }
        public virtual void ChangeColor()
        {
            
            mat.color = Color.red;
            StartCoroutine(ColorIsWaiting());
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ball")) {
                col.enabled = false;
                gameManager.AddHit(1);
                ChangeColor();
            }

           
        }
      
        IEnumerator ColorIsWaiting()
        {
            yield return new WaitForSeconds(1);
            mat.color= Color.white;
            col.enabled = true;

        }
    }
}

