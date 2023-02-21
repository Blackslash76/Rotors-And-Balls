using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleratore : MonoBehaviour
{
    private int DirezioneX;
    private int DirezioneY;

    public float SpeedMultiplier = 2.0f;

    private void Awake()
    {
        int _tmp = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (_tmp)
        {
            case 0:
                {
                    DirezioneX = 0;
                    DirezioneY = 1;
                    break;
                }
            case 90:
                {
                    DirezioneX = -1;
                    DirezioneY = 0;
                    break;
                }
            case 180:
                {
                    DirezioneX = 0;
                    DirezioneY = 1;
                    break;
                }
            case 270:
                {
                    DirezioneX = 1;
                    DirezioneY = 0;
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
            if ((ball.directionX != 0 && ball.directionX == -DirezioneX) || (ball.directionY != 0 && ball.directionY == -DirezioneY))
            {
                ball.ScattaScintilla();
                ball.ResumeBallMovement(-ball.directionX, -ball.directionY, Main.Level.LevelSpeedCircuito);
            }
            else
            {
                ball.ResumeBallMovement(DirezioneX, DirezioneY, Main.Level.LevelSpeedCircuito * SpeedMultiplier);
            }
        }
    }

}
