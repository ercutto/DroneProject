using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PinBall
{
    public class UIController : MonoBehaviour
    {
        public GameManager gameManager;
        public GameObject 
            StartUI,
            GameOverUI,         
            TriggerButton,
            UiAnimation,
            startPanel;
        public bool isPushed;
       

        // Start is called before the first frame update
        void Start()
        {
            isPushed = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ActiveOrFalse(GameObject obj)
        {
            
            if (obj.activeInHierarchy) { obj.SetActive(false); } else { obj.SetActive(true); }
            
        }
        public void StartGame()//and Restart
        {
            if (!isPushed) { isPushed = true; if (isPushed) { ActiveOrFalse(startPanel); ActiveOrFalse(UiAnimation); StartCoroutine(nameof(StartAfterTime), StartUI);} }
           
            
            

        }
        public void RestartGame()
        {

            if(!isPushed){ isPushed = true; if(isPushed) { StartCoroutine(nameof(StartAfterTime), GameOverUI); } }
                
                
        }
        IEnumerator StartAfterTime(GameObject obj)
        {
            gameManager.ResetGameVariables();
            yield return new WaitForSeconds(3);
            ActiveOrFalse(obj);
            isPushed = false;
        }
        public void CloseApp()
        {
            Application.Quit();
        }
       
        
    }
}

