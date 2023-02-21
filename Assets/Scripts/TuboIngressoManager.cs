using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboIngressoManager : MonoBehaviour {

    public int DirezioneX = 0;
    public int DirezioneY = -1;

    private BallManager ball;
    private ScriptInnesto innesto;
    private Rotore rotore;
    private bool PiattaformaTuboScambio = false;
    

	// Use this for initialization
	void Start () {
		if (DirezioneX == -99)
        {
            //Per compatibilità con la vecchia start line ho aggiunto questa condizione -99 che
            //decide in autonomia la rotazione X e Y dell'Ingresso
            switch (Mathf.RoundToInt(transform.eulerAngles.z))
                {
                case 0:
                    {
                        DirezioneX = 1;
                        DirezioneY = 0;
                    }
                    break;
                case 90:
                    {
                        DirezioneX = 0;
                        DirezioneY = 1;
                    }
                    break;
                case 180:
                    {
                        DirezioneX = -1;
                        DirezioneY = 0;
                    }
                    break;
                case 270:
                    {
                        DirezioneX = 0;
                        DirezioneY = -1;
                    }
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Caricatore"))
        {
            
            rotore = collision.transform.GetComponent<Rotore>();
        }

        if (collision.transform.CompareTag("Innesto"))
        {
            
            innesto = collision.transform.GetComponent<ScriptInnesto>();
        }

        if (collision.transform.CompareTag("TuboScambio,Piattaforma"))
        {
            PiattaformaTuboScambio = true;

        }

        if (collision.transform.tag.Contains("Ball,"))
        {
            ball = collision.transform.GetComponent<BallManager>();


            //Vediamo se la pallina può partire

            if (rotore && innesto && ball)
            {
                if (!rotore.IsRotating && !innesto.SlotOccupato && ball.PrimoInnesto)
                {
                    collision.transform.position = this.transform.position;
                    ball.ResumeBallMovement(DirezioneX, DirezioneY, Main.Level.LevelSpeedPerimetro);
                    rotore.NewBallIncoming = true;
                }
            }

            if (PiattaformaTuboScambio && ball)
            {
                Debug.Log("Scambio accettato");
                collision.transform.position = this.transform.position;
                ball.ResumeBallMovement(DirezioneX, DirezioneY, Main.Level.LevelSpeedPerimetro);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Caricatore"))
        {
            rotore = null;
        }

        if (collision.transform.tag.Contains("Ball,"))
        {
            ball = null;
        }

        if (collision.transform.CompareTag("Innesto"))
        {
            innesto = null;
        }
        if (collision.transform.CompareTag("TuboScambio,Piattaforma"))
        {
            PiattaformaTuboScambio = false;
        }

    }
}
