using System.Collections;

using UnityEngine;
namespace PinBall
{
    public class ExtraBonusGroups :CollCheck
    {
        
        public GameObject pushObject;
        public GroubPointsCollect groubPointsCollect;
        public GroubAnimCont groubAnimCont;
        private Color red = Color.red;
        private Color white = Color.white;
        private Color green = Color.green;


        public override void ScoreAndAction()
        {
           
            ColliderControll(false);
            groubAnimCont.Anim();
            groubPointsCollect.CountBonus(1);
            Change(red);
        }
        public void BackToStart()
        {
           
            Change(white);
            StartCoroutine(ColorDance());
        }
        IEnumerator ColorDance()
        { 
            yield return new WaitForSeconds(0.3f);
            Change(green);
            yield return new WaitForSeconds(0.3f);
            Change(white);
            ColliderControll(true);
            



        }
        

    }
}

