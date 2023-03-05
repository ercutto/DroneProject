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
        public AudioSource audioSource;
        public AudioClip _clip,wellcome,keepers;
        
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
            if (!isPushed) { isPushed = true; if (isPushed) { PlaySounds(_clip); ActiveOrFalse(startPanel); ActiveOrFalse(UiAnimation); StartCoroutine(nameof(StartAfterTime), StartUI);} }
           
            
            

        }
        public void RestartGame()
        {

            if(!isPushed){ isPushed = true; if(isPushed) { PlaySounds(_clip); StartCoroutine(nameof(StartAfterTime), GameOverUI); } }
                
                
        }
        IEnumerator StartAfterTime(GameObject obj)
        {
            gameManager.ResetGameVariables();
            yield return delay;
            ActiveOrFalse(obj);
            PlaySounds(wellcome);
            isPushed = false;
        }
        public void CloseApp()
        {
            Application.Quit();
        }
        public void PlaySound()//ui
        {
            PlaySounds(_clip);
           
        }
       public void PlayKeeperSound()
        {
            PlaySounds(keepers);
        }
        public void PlaySounds(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
       


    }
}

