using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboScambio : MonoBehaviour {

    private GameObject Freccia;

    private float DirezioneX;
    private float DirezioneY;

    public int NumeroDiLati;

    private void Awake()
    {
        Freccia = transform.Find("Pallazza/Freccia").gameObject;
        DirezioneX = Freccia.transform.GetComponent<FrecciaTuboScambio>().DirezioneX;
        DirezioneY = Freccia.transform.GetComponent<FrecciaTuboScambio>().DirezioneY;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Ball,"))
        {
            if (collision.transform.position != transform.position)
            {
                collision.transform.position = transform.position;
            }

            DirezioneX = Freccia.transform.GetComponent<FrecciaTuboScambio>().DirezioneX;
            DirezioneY = Freccia.transform.GetComponent<FrecciaTuboScambio>().DirezioneY;

            
            BallManager ball = collision.GetComponent<BallManager>();


            ball.ResumeBallMovement(DirezioneX, DirezioneY, Main.Level.LevelSpeedCircuito);
            //A volte uscendo da un rotore la pallina non risulta innestabile, con questo comando la rendo tale
            ball.Innestabile = true;
        }
    }

    public void RuotaFreccia()
    {
        Freccia.transform.GetComponent<FrecciaTuboScambio>().Rotate();
    }
}
