using System.Collections;

using UnityEngine;
namespace PinBall
{
    public class ExtraBonusGroups :CollCheck
    {
        
        public GameObject pushObject;
        public GroubPointsCollect groubPointsCollect;
        public GroubAnimCont groubAnimCont;
    
        
       
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        

        public override void ScoreAndAction()
        {
            ColliderControll(false);
            groubAnimCont.IsTouched();
            groubPointsCollect.CountBonus(1);
            Change(Color.red);
        }
        public void BackToStart()
        {
           
            Change(Color.white);
            StartCoroutine(ColorDance()); 
            
        }
        IEnumerator ColorDance()
        { 
            yield return delay;
            Change(Color.green);
            yield return delay;
            Change(Color.white);
            ColliderControll(true);
            



        }
        

    }
}

