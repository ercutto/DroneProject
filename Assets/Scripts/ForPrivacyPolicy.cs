using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PinBall
{

    public class ForPrivacyPolicy : MonoBehaviour
    {
        int trackOrNot=0;//default
        public GameObject myPolicy;
        public Button[] buttons;
        public Color DefaultColor;
        public Color DisabledColor;
        WaitForSeconds delay=new WaitForSeconds(3);
        public ParticleSystem[] particleSystems;
        // Start is called before the first frame update
        public void Awake()
        {
            PlayerPrefs.SetInt("trackOrNot", 0);
            trackOrNot = PlayerPrefs.GetInt("trackOrNot", trackOrNot);
            if (trackOrNot == 1)
            {
                myPolicy.SetActive(false);
            }
            else
            {
                myPolicy.SetActive(true);
            }
        }
        void Start()
        {
            
            
        }
        public void MoreInformation()
        {
           
            Application.OpenURL("https://ercuttogamelay.blogspot.com/2023/05/pixeldev-in-our-apps-and-games-we-use.html");
        }
        public void AcceptAll(int currentState)
        {
            myPolicy.SetActive(false);
            trackOrNot = currentState;
            PlayerPrefs.SetInt("trackOrNot", trackOrNot);
            foreach (var prsystm in particleSystems)
            {
                prsystm.Play();
            }

        }
        public void Deciline()
        {
            Application.Quit();
        }
        public void CallCoroutine()
        {
            StartCoroutine(ButtonsEnabled());
        }
        IEnumerator ButtonsEnabled()
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Image>().color = DisabledColor;
                button.enabled = true;
            }
            yield return delay;
            foreach (var button in buttons)
            {
                button.GetComponent<Image>().color = DefaultColor;
                button.enabled = true;
            }
        }
    }
}

