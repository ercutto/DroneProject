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
        public bool AddMobVideoIsPlayed;
        public Text fps;
        public AudioSource audioSource;
        public AudioSource HandlesAndHigVolumes;
        public AudioClip _clip,wellcome,keepers;
        public ChangeCurrents changeCurrents;
        WaitForSeconds delay =new WaitForSeconds(3f);
        WaitForSeconds videoTime =new WaitForSeconds(3f);
        


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
            if (!isPushed) { isPushed = true; if (isPushed) { PlaySounds( audioSource, _clip); ActiveOrFalse(startPanel); ActiveOrFalse(UiAnimation); StartCoroutine(nameof(StartAfterTime), StartUI);} }
           
            
            

        }
        //public void ContinueGameAfterRevard()
        //{
        //    gameManager.PlayerSelectToContinue();
        //}
        public void RestartGame()
        {

            if(!isPushed){ isPushed = true; if(isPushed) { PlaySounds(audioSource, _clip); StartCoroutine(nameof(RestartAfterTime), GameOverUI); } }
                
                
        }
        IEnumerator RestartAfterTime(GameObject obj)
        {
            //gameManager.ResetGameVariables();
            changeCurrents.SetBack();
            yield return delay;
            ActiveOrFalse(obj);
            gameManager.ResetGameVariables();
            PlaySounds(audioSource, wellcome);
            isPushed = false;
        }
        IEnumerator StartAfterTime(GameObject obj)
        {
            gameManager.ResetGameVariables();      
            yield return delay;
            ActiveOrFalse(obj);
            PlaySounds(audioSource, wellcome);
            isPushed = false;
        }
        public void CloseApp()
        {
            Application.Quit();
        }
        public void PlaySound()//ui
        {
            PlaySounds(audioSource, _clip);
           
        }
       public void PlayKeeperSound()
        {
            PlaySounds(HandlesAndHigVolumes, keepers);
        }
        public void PlaySounds(AudioSource source, AudioClip clip)
        {
            source.PlayOneShot(clip);
        }
       
        public void AddMobVideoPlay()
        {
            StartCoroutine(PlayVideo());
        }
        
        IEnumerator PlayVideo()
        {
            PlayingVideo();
            yield return new WaitUntil(()=>AddMobVideoIsPlayed);

        }
        void PlayingVideo()
        {

        }

    }
}

