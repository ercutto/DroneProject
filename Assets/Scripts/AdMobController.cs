using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
namespace PinBall {
    public class AdMobController : MonoBehaviour
    {
        private InterstitialAd interstitialAd;
        public string adUnitId = "";
        private WaitForSeconds waiting=new WaitForSeconds(0.5f);
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }
        void RequestAndLoadInstertitialId()
        {
           

           
        }
        public void ShowInterstitialAd()
        {
           
            StartCoroutine(Wait());
        }
        IEnumerator Wait()
        {
            yield return waiting;
            RequestAndLoadInstertitialId();
        }
    }
   
}

