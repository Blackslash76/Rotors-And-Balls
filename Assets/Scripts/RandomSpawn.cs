using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {

    public bool debugMode;
    public GameObject[] ballsList;
    public GameObject[] specialBalls;

    public float[] specialBallsChance;

    public float directionBallX = 1;
    public float directionBallY = 0;


    private GameObject ball;
    private Vector2 whereToSpawn;

    private void Start()
    {
        //SpawnBall();
    }

    public void SpawnBall()
    {
        ScegliPalla();
    }


    void ScegliPalla()
            
    {
        float randomvalue;
        int specialball = -1;

        if (specialBalls.Length > 0)
        {
            //Prima Lancio i Dadi per le Palle Speciali
            for (int i = 0; i < specialBalls.Length; i++)
            {
                randomvalue = Random.Range(0, 100);
                if (randomvalue <= specialBallsChance[i])
                {
                    specialball = i;
                    break;
                }
            }
        }

        if ( specialball == -1)
        {
            //Se nessuna palla speciale è stata scelta, spawno una palla normale
            ball = ballsList[Random.Range(0, ballsList.Length)];
        }
        else
        {
            ball = specialBalls[specialball];
        }

        whereToSpawn = new Vector2(transform.position.x, transform.position.y);

       GameObject go = Instantiate(ball, whereToSpawn, Quaternion.identity);

       go.transform.GetComponent<BallManager>().ResumeBallMovement(directionBallX, directionBallY, Main.Level.LevelSpeedPerimetro);
    }

    public void DebugChangeFrequencySpecialBalls()
    {
        specialBallsChance[0] = 100;
    }
}
