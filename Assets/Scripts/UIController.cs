using System.Collections;

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
            triggerContainer,
            UiAnimation,
            startPanel;
        public bool isPushed;
        public Text fps;
        
        WaitForSeconds delay =new WaitForSeconds(3f);
       

        // Start is called before the first frame update
        void Start()
        {
            isPushed = false;
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
            yield return delay;
            ActiveOrFalse(obj);
            isPushed = false;
        }
        public void CloseApp()
        {
            Application.Quit();
        }
       
        
    }
}

