using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    /// <summary>
    /// Main ball can be spawned only maxball amount from mainspawnPos and when its hits to reflector(reflector has bool "SpawnBall" hasto be true) can spawn new ball.
    /// extraball can be spawned as from extra ball spawnPos,can not spawn new ball and doesnt effects to maxball. 
    /// </summary>
    public class Extras : MonoBehaviour
    {
        GameManager gameManager;
        public GameObject basic_ball,clone;
        public Material extraBallMaterial;
        public GameObject spawnExtraBall_Pos, spawnMainBall_Pos;
        public bool isSpawned,isMainBallSpawned,ballFinished;
        
        // Start is called before the first frame update
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            isSpawned = true;
            isMainBallSpawned = true;
         
            Spawnball();
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
                clone= Instantiate(basic_ball, spawnExtraBall_Pos.transform.position, transform.rotation);
                clone.GetComponent<BallHit>().thisIsMainBall = false;
                clone.GetComponent<MeshRenderer>().material = extraBallMaterial;
                gameManager.TotalBallCount(1);
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
                    gameManager.TotalBallCount(1);

                }
                isMainBallSpawned = true;
            }
            
        }

    }
}
