using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptInnesto : MonoBehaviour {

    public int posizione;
    bool UscitaPalla = false;

    public bool SlotOccupato = false;

    enum Direzione
    {
        top,
        left,
        down,
        right
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Contains("MassAreaPalline"))
        {
            BallManager ballScript = collision.transform.parent.GetComponent<BallManager>();
            if (ballScript.Innestabile && SlotOccupato)
            {
                //Slot già occupato
                ballScript.ResumeBallMovement(-ballScript.directionX, -ballScript.directionY, Main.Level.LevelSpeedCircuito);
                ballScript.ScattaScintilla();
            }
        }

        if (collision.tag.Contains("Ball,"))
        {
            InnestaPallaSemplice(collision);

            if (collision.CompareTag("Ball,Rainbow"))
            {
                InnestaPallaRainbow(collision);
            }
        }
        

        if (collision.CompareTag("Tubo,Orizzontale"))
        {
            UscitaPalla = true;
        }

        if (collision.CompareTag("Tubo,Verticale"))
        {
            UscitaPalla = true;
        }

        if (collision.CompareTag("TuboScambio,Piattaforma"))
        {
            UscitaPalla = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag ("Tubo,Orizzontale"))
        {
            UscitaPalla = false;
        }

        if (collision.CompareTag("Tubo,Verticale"))
        {
            UscitaPalla = false;
        }

        if (collision.CompareTag("TuboScambio,Piattaforma"))
        {
            UscitaPalla = false;
        }
    }

    public void SganciaBall (Transform ballobj)
    {
        BallManager ballScript = ballobj.GetComponent<BallManager>();

        //In base alla rotazione della palla imposto la direzione

        if (UscitaPalla)
        {
            switch (posizione)
            {
                case 0:
                    ballScript.ResumeBallMovement(0, 1, Main.Level.LevelSpeedCircuito);
                    break;
                case 1:
                    ballScript.ResumeBallMovement(-1, 0, Main.Level.LevelSpeedCircuito);
                    break;
                case 2:
                    ballScript.ResumeBallMovement(0, -1, Main.Level.LevelSpeedCircuito);
                    break;
                case 3:
                    ballScript.ResumeBallMovement(1, 0, Main.Level.LevelSpeedCircuito);
                    break;
            }
            SlotOccupato = false;
        }

    }

    private void InnestaPallaSemplice(Collider2D collision)
    {
        Rotore RotoreScript = this.transform.parent.GetComponent<Rotore>();
        BallManager ballScript = collision.GetComponent<BallManager>();
        if (ballScript.Innestabile && !SlotOccupato)
        {
            // Riattivo lo spawner
            if (ballScript.PrimoInnesto)
            {
                if (!Main.Tutorial.itsTutorial)
                {
                    ballScript.PrimoInnesto = false;
                    GameObject go = GameObject.FindWithTag("Spawner");
                    if (go)
                    {
                        SpawnerData.StartSpawner();

                        Main.Player.AddScore(50);

                    }
                }

                else
                {
                    //Il tutorial usa il vecchio spawner
                    ballScript.PrimoInnesto = false;
                    GameObject go = GameObject.FindWithTag("Spawner");
                    if (go != null)
                    {
                        go.GetComponent<RandomSpawn>().SpawnBall();
                    }
                }

            }

            ballScript.StopBall();

            collision.transform.SetParent(this.transform);
            collision.transform.localPosition = new Vector2(0, 0);
            ballScript.Innestabile = false;
            SlotOccupato = true;
            RotoreScript.NewBallIncoming = false;

            //Rimappo la collisione
            Quaternion _tmp = Quaternion.AngleAxis(90 + (posizione * 90), new Vector3(0, 0, 1));
            collision.transform.Find("TouchArea").rotation = _tmp;

            //Zoomo la palla
            ballScript.ZoomBall();

            //Play the sound;
            Main.Audio.PlaySound(Main.Audio.Suoni.InnestoPalla);

            //Verifico se ho completato tutto il rotore
            
            RotoreScript.VerificaCaricatore();
        }
    }
    private void InnestaPallaRainbow(Collider2D collision)
    {
        collision.GetComponent<RainbowBall_Script>().VerificaDopoInnesto();
    }
}
