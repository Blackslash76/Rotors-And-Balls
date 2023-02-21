using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnerData
{
    private static List<GameObject> StandardBalls = new List<GameObject>();
    private static GameObject NextBall;
    private static GameObject[] SpawnersInLevel;
    private static GameObject CurrentSpawner;

    public static bool itsReady = true;
    public static bool nextBallReady = false;

    public static void Init()
    {
        if (StandardBalls.Count == 0)
        {
            //Popolo le standard balls
            StandardBalls.Add(SkinManager.GetObject("BallRed"));
            StandardBalls.Add(SkinManager.GetObject("BallGreen"));
            StandardBalls.Add(SkinManager.GetObject("BallBlue"));
            StandardBalls.Add(SkinManager.GetObject("BallYellow"));
        }
    }

    public static void UpdateLevelData()
    {
        SpawnersInLevel = GameObject.FindGameObjectsWithTag("Spawner");

        GenerateNextBall(SpawnersInLevel[0].GetComponent<Spawner>().NumeroDiPalline);

        foreach (GameObject spawner in SpawnersInLevel)
        {
            if (spawner != CurrentSpawner)
                {
                spawner.GetComponent<Animator>().Play("Spawner_startlevel");
                }
        }
    }

    public static GameObject GetNextBall()
    {
        return NextBall;
    }

    public static void GenerateNextBall(int numeropalline)
    {
        NextBall = StandardBalls[Random.Range(0, numeropalline)];
        CurrentSpawner = SpawnersInLevel[Random.Range(0, SpawnersInLevel.Length)];
        CurrentSpawner.transform.Find("Bandiera").GetComponent<SpriteRenderer>().color = GetNextBallColor();
    }

    public static Color GetNextBallColor()
    {
        return NextBall.GetComponent<BallManager>().coloreBall;
    }

    public static GameObject GetCurrentSpawner()
    {
        if (CurrentSpawner == null)
        {
            return GameObject.FindGameObjectsWithTag("Spawner")[0];
        }
        else
        {
            return CurrentSpawner;
        }
    }

    public static void InitSpawner(float TimeToWait)
    {
        CurrentSpawner.GetComponent<Spawner>().InitSpawner(TimeToWait);
    }
    public static void StartSpawner()
    {
        if (SpawnerData.itsReady)
        {
            CurrentSpawner.GetComponent<Spawner>().SpawnBall();
        }
        else
        {
            nextBallReady = true;
        }
    }

    public static GameObject ReturnStandardBallByIndex(int index)
    {
        return StandardBalls[index];
    }



}
