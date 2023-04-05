
using UnityEngine;
namespace PinBall {
    public class Restarter : MonoBehaviour
    {
        public Boss boss;
        public GameObject[] toRestart;

        public void RestartAll()
        {
            foreach (var item in toRestart)
            {
                if (item.GetComponent<GroubPointsCollect>() != null)
                {
                    item.GetComponent<GroubPointsCollect>().ObjectsBactToStart();
                    item.GetComponent<GroubPointsCollect>().AfterRestart();

                }
                //if(item.GetComponent<AnimWithTrigger>()!=null) { item.GetComponent<AnimWithTrigger>().SetBack(); }
                
               
            }

            boss.bossHealth = 0;
        }
    }
}

