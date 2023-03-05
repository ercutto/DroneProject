
using UnityEngine;
using UnityEngine.UI;
namespace PinBall
{
    public class GroubPointsCollect : MonoBehaviour
    {
        private int exBonus;
        public int Score,currentScore;
        private int scoreMulipiler,count;
        private GameManager gameManager;
        public ExtraBonusGroups[] extraBonusGroups;
        public GameObject bonusCanvasPrefab;
        public Text ObjectScore;
        
        // Start is called before the first frame update
        void Start()
        {
            ObjectScore = bonusCanvasPrefab.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            bonusCanvasPrefab.SetActive(false);
            gameManager = FindObjectOfType<GameManager>();
            AfterRestart();
            
        }
        public void CountBonus(int addBonus)
        {
            if (exBonus < extraBonusGroups.Length)
            { exBonus += addBonus;
                if (exBonus == extraBonusGroups.Length)
                {
                    exBonus = 0;
                    CalculateScore();
                    ObjectsBactToStart();
                    bonusCanvasPrefab.SetActive(true);
                    Invoke(nameof(CanvasFalse),1);
                }
            } 
        }
        public void ObjectsBactToStart()
        {
            foreach (var item in extraBonusGroups)
            {
                if(item.gameObject.activeInHierarchy){ item.BackToStart(); }
                
                
            }

            
        }
        
        void CanvasFalse()
        {              
            bonusCanvasPrefab.SetActive(false);
        }
        void CalculateScore()
        {
            count += 1;
            if (count >= 3)
            {
                
                scoreMulipiler += 500;
                currentScore += scoreMulipiler;
            }
          
            ObjectScore.text= currentScore.ToString();
            gameManager.AddScore(currentScore);
        }
        public  void AfterRestart()
        {
            exBonus = 0;
            scoreMulipiler = 0;
            count = 0;
            currentScore = Score;
        }

    }

}

