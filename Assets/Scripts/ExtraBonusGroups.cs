using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class ExtraBonusGroups :CollCheck
    {
        
        public GameObject pushObject;
        public GroubPointsCollect groubPointsCollect;
        public GroubAnimCont groubAnimCont;

        

        public override void ScoreAndAction()
        {
           
            ColliderControll(false);
            groubAnimCont.Anim();
            groubPointsCollect.CountBonus(1);
            Change(Color.red);
        }
        public void BackToStart()
        {
            Debug.Log("oldu");
            Change(Color.white);
            StartCoroutine(ColorDance());
        }
        IEnumerator ColorDance()
        { 
            yield return new WaitForSeconds(0.3f);
            Change(Color.green);
            yield return new WaitForSeconds(0.3f);
            Change(Color.white);
            ColliderControll(true);
            



        }
        

    }
}

