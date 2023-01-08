using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class GroubPointsCollect : MonoBehaviour
    {
        public int exBonus;
        private GameManager gameManager;
        public ExtraBonusGroups[] extraBonusGroups;
        
        // Start is called before the first frame update
        void Start()
        {
           
            gameManager = FindObjectOfType<GameManager>();
            exBonus = 0;
        }
        public void CountBonus(int addBonus)
        {
            if (exBonus < extraBonusGroups.Length)
            { exBonus += addBonus;
                if (exBonus == extraBonusGroups.Length)
                {
                    exBonus = 0;
                    gameManager.AddScore(500);
                    ObjectsBactToStart();

                }
            } 
        }
        void ObjectsBactToStart()
        {
            foreach (var item in extraBonusGroups)
            {
                item.BackToStart();
            }
        }
        
    }

}

