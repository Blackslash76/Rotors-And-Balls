using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboSemaforo : MonoBehaviour {


    public List<string> BallsAllowed;
    public Color coloreLampadina;

    private void Awake()
    {
        transform.GetComponent<SpriteRenderer>().color = coloreLampadina;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Ball,") && !BallsAllowed.Contains(collision.tag))
        {
            //Se la palla non è in lista
            BallManager ballscript = collision.GetComponent<BallManager>();
            ballscript.ResumeBallMovement(-ballscript.directionX, -ballscript.directionY, Main.Level.LevelSpeedCircuito);
            //anim.Play("TuboSemaforo_LightOnOff");
            Main.Audio.PlaySound(Main.Audio.Suoni.Divieto);

        }
    }


}
