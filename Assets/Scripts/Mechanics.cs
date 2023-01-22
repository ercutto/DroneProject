
using UnityEngine;
namespace PinBall
{
    /// <summary>
    /// Main ball can be spawned only maxball amount from mainspawnPos and when its hits to reflector(reflector has bool "SpawnBall" hasto be true) can spawn new ball.
    /// extraball can be spawned as from extra ball spawnPos,can not spawn new ball and doesnt effects to maxball. 
    /// </summary>
    public class Mechanics : MonoBehaviour
    {
        GameManager gameManager;
        public GameObject basic_ball,clone;
        public Material extraBallMaterial;
        public GameObject spawnExtraBall_Pos, spawnMainBall_Pos;
        public bool isSpawned,isMainBallSpawned,ballFinished;
        public GameObject[] ControlledObjects;
        
        // Start is called before the first frame update
        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            isSpawned = true;
            isMainBallSpawned = true;
            Spawnball();
            InvokeRepeating(nameof(Control_Objects), 1, 3);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Spawnball()
        {
            if (isSpawned) { return; }
            else
            {
                if (gameManager.totalAmountOfBall < 2)
                {
                    clone = Instantiate(basic_ball, spawnExtraBall_Pos.transform.position, transform.rotation);
                    clone.GetComponent<BallHit>().thisIsMainBall = false;
                    clone.GetComponent<MeshRenderer>().material = extraBallMaterial;
                    gameManager.TotalBallCount(1);
                }
               
            }
            isSpawned = true;


        }
        
        public void Spawnball_Main()
        {
            if (!gameManager.ballFinished) {
                if (isMainBallSpawned) { return; }
                else
                {
                    clone = Instantiate(basic_ball, spawnMainBall_Pos.transform.position, transform.rotation);
                    clone.GetComponent<BallHit>().thisIsMainBall = true;
                    clone.name = "mainBall";
                    gameManager.TotalBallCount(1);

                }
                isMainBallSpawned = true;
            }
            
        }
        void Control_Objects()
        {
            int rand = Random.Range(0, ControlledObjects.Length);
            foreach (var item in ControlledObjects)
            {
                if(item!= ControlledObjects[rand])
                {
                    item.GetComponent<Animator>().SetTrigger("open");

                }
                else
                {
                    item.GetComponent<Animator>().SetTrigger("close");



                }

            }
            
        }

    }
}

