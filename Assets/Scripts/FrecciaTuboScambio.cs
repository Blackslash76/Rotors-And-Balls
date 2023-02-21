using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrecciaTuboScambio : MonoBehaviour {
    private int DirezioneFreccia;

    public float DirezioneX;
    public float DirezioneY;

    private int NumeroDiLati;
    private int RotazioneIniziale;

    private void Awake()
    {
        transform.tag = "Freccia";

        NumeroDiLati = transform.parent.parent.GetComponent<TuboScambio>().NumeroDiLati;
        RotazioneIniziale = Mathf.RoundToInt(transform.eulerAngles.z);
        DirezioneFreccia = 0;
        RicalcolaDirezioneFreccia();

        //Sistemo l'angolatura della pallazza che altrimenti risulterebbe ruotata come da editor
        Transform pallazza = transform.parent;
        transform.SetParent(null);
        pallazza.rotation = Quaternion.identity;
        transform.SetParent(pallazza);
        //**************************************************************************************
    }

    public void Rotate()
    {
        if (DirezioneFreccia == NumeroDiLati - 1)
        {
            DirezioneFreccia = 0;
        }
        else
        {
            DirezioneFreccia += 1;
        }

        transform.localRotation = Quaternion.Euler(0, 0, (DirezioneFreccia * 90) + RotazioneIniziale);
        RicalcolaDirezioneFreccia();
    }

    private void RicalcolaDirezioneFreccia()
    {
        int _tmp = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (_tmp)
        {
            case 0:
                {
                    DirezioneX = 1;
                    DirezioneY = 0;
                    break;
                }
            case 90:
                {
                    DirezioneX = 0;
                    DirezioneY = 1;
                    break;
                }
            case 180:
                {
                    DirezioneX = -1;
                    DirezioneY = 0;
                    break;
                }
            case 270:
                {
                    DirezioneX = 0;
                    DirezioneY = -1;
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Contains("Ball,"))
        {

            BallManager ball = collision.GetComponent<BallManager>();
            //vediamo se fare la scintilla
            if ((ball.directionX!=0 && ball.directionX == -DirezioneX) || (ball.directionY!=0 && ball.directionY == -DirezioneY))
            {
                ball.ScattaScintilla();
            }
            ball.ResumeBallMovement(DirezioneX, DirezioneY, Main.Level.LevelSpeedCircuito);
        }

    }
}
