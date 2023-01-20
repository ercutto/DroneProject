
using UnityEngine;
using UnityEngine.UI;
namespace PinBall
{
    public class GroubPointsCollect : MonoBehaviour
    {
        private int exBonus;
        public int Score;
        private GameManager gameManager;
        public ExtraBonusGroups[] extraBonusGroups;
        public GameObject bonusCanvasPrefab;
        
        // Start is called before the first frame update
        void Start()
        {
            bonusCanvasPrefab.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Score.ToString();
            bonusCanvasPrefab.SetActive(false);
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
                    gameManager.AddScore(Score);
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
                item.BackToStart();
                
            }
        }
        
        void CanvasFalse()
        {              
            bonusCanvasPrefab.SetActive(false);
        }

    }

}

