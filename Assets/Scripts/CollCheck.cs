using System.Collections;
using UnityEngine;
namespace PinBall {
    public class CollCheck : MonoBehaviour
    {
     
        private Material mat;
        public Collider col;
        private GameManager gameManager;
        public bool isTouched;
        public Color red = Color.yellow;
        public Color white = Color.white;
        private string _tag = "ball";
        WaitForSeconds delay = new WaitForSeconds(1);
        public AudioSource effectAudioSource;
        public AudioClip clip;

     

        private void Start()
        {
            isTouched = false;
            gameManager = FindObjectOfType<GameManager>();      
            col = GetComponent<BoxCollider>();
            mat = GetComponent<MeshRenderer>().material;
            

        }
       
        public virtual void ChangeColor()
        {
         
                StartCoroutine(ColorIsWaiting());
           
 
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
            
                ScoreAndAction();
                effectAudioSource.PlayOneShot(clip);

            }

        }

        public virtual void ScoreAndAction()
        {
            //col.enabled = false;

            //col.enabled = false;
            StartCoroutine(ColorIsWaiting());
               

            //gameManager.AddHit(1);
        }
      
        IEnumerator ColorIsWaiting()
        {
            ColliderControll(false);
            Change(red);
            yield return delay;
            Change(white);
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


