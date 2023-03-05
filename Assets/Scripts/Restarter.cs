
using UnityEngine;
namespace PinBall {
    public class Restarter : MonoBehaviour
    {
        public GameObject[] toRestart;

        public void RestartAll()
        {
            foreach (var item in toRestart)
            {
                item.GetComponent<GroubPointsCollect>().ObjectsBactToStart();
                item.GetComponent<GroubPointsCollect>().AfterRestart();
            }
        }
    }
}

