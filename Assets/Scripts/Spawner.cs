using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int NumeroDiPalline = 2;
    public int direzionePallaX = 0;
    public int direzionePallaY = 0;

    private Animator anim;

    private void Awake()
    {
        anim = transform.GetComponent<Animator>();

    }

    public void InitSpawner(float TimeToWait)
    {
        StartCoroutine(WaitBeforeSpawn(TimeToWait));
    }
    public void SpawnBall()
    {
        SpawnerData.itsReady = false;
        SpawnerData.nextBallReady = false;

        anim.Play("Spawner_Rimpicciolisci");
    }

    private void EffettoSpecialeEPoiPalla()
    {
        GameObject ball;
        ball = SpawnerData.GetNextBall();

        //Animazione fuochi d'artificio
        GameObject particle = transform.Find("Esplosione").gameObject;
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = ball.GetComponent<BallManager>().coloreBall;
        particle.SetActive(true);

        EsciPalla();

    }

    public void EsciPalla()
    {
        GameObject ball;
        ball = SpawnerData.GetNextBall();

        Vector2 whereToSpawn = new Vector2(transform.position.x, transform.position.y);

        GameObject go = Instantiate(ball, whereToSpawn, Quaternion.identity);
        go.transform.GetComponent<BallManager>().ResumeBallMovement(direzionePallaX, direzionePallaY, Main.Level.LevelSpeedPerimetro);

        SpawnerData.GenerateNextBall(NumeroDiPalline);

        SpawnerData.InitSpawner(0.75f);
    }

    public void PlayIdleAnimation()
    {

        SpawnerData.itsReady = true;

        if (SpawnerData.nextBallReady)
        {
            SpawnBall();
        }
        else
        {
            anim.Play("Spawner_idle");
        }
    }

    IEnumerator WaitBeforeSpawn(float time)
    {
        yield return new WaitForSeconds(time);

        transform.Find("Bandiera").GetComponent<SpriteRenderer>().color = SpawnerData.GetNextBallColor();
        anim.Play("Spawner_Ingrandisci");
    }
}
