using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Angolo : MonoBehaviour {

    public int direzione;
    // Use this for initialization
    void Start () {
        if (direzione == -99)
        {
            //Per compatibilità con la vecchia start line ho aggiunto questa condizione -99 che
            //decide in autonomia l'angolo di rotazione della pallina

            
            switch (Mathf.RoundToInt(transform.rotation.eulerAngles.z))
            {
                case 0:
                    {
                        direzione = 1;
                    }
                    break;
                case 90:
                    {
                        direzione = -1;
                    }
                    break;
                case 180:
                    {
                        direzione = 1;
                    }
                    break;
                case 270:
                    {
                        direzione = -1;
                    }
                    break;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Ball,"))
        {
            BallManager ball = collision.GetComponent<BallManager>();

            float old_x = ball.directionX;
            float old_y = ball.directionY;
            collision.transform.position = this.transform.position;
            ball.ResumeBallMovement(old_y * direzione, old_x * direzione, ball.speed);
        }
    }
}
