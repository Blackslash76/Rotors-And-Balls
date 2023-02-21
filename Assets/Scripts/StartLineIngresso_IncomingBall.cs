using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineIngresso_IncomingBall : MonoBehaviour
{
    int direzione;
    int DirezioneX;
    int DirezioneY;

    StartLineIngresso_Conditions conditions;
    // Start is called before the first frame update
    void Start()
    {
        conditions = transform.parent.GetComponent<StartLineIngresso_Conditions>();

        switch (Mathf.RoundToInt(transform.parent.transform.eulerAngles.z))
        {
            case 0:
                {
                    direzione = -1;
                    DirezioneX = 1;
                    DirezioneY = 0;
                }
                break;
            case 90:
                {
                    direzione = 1;
                    DirezioneX = 0;
                    DirezioneY = 1;
                }
                break;
            case 180:
                {
                    direzione = -1;
                    DirezioneX = -1;
                    DirezioneY = 0;
                }
                break;
            case 270:
                {
                    direzione = 1;
                    DirezioneX = 0;
                    DirezioneY = -1;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Contains("Ball,"))
        {
            BallManager ball = collision.GetComponent<BallManager>();

            float old_x = ball.directionX;
            float old_y = ball.directionY;



            if (conditions.VerificaVarcoAttivo(collision.transform))
            {
                float new_x = DirezioneX;
                float new_y = DirezioneY;

                collision.transform.position = this.transform.position;
                ball.ResumeBallMovement(new_x, new_y, Main.Level.LevelSpeedPerimetro);
                ball.InseritaNelCircuito = true;
            }

            else if (ball.InseritaNelCircuito)
                {
                float new_x = old_y * direzione;
                float new_y = old_x * direzione;

                collision.transform.position = this.transform.position;
                ball.ResumeBallMovement(new_x, new_y, Main.Level.LevelSpeedPerimetro);
                ball.InseritaNelCircuito = false;
            }

        }
    }
}
