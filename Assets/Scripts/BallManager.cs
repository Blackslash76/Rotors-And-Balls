using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    public float speed;    // Here we set a float variable to hold our speed value

    public string DescrizioneFunzione = "";

    public GameObject Scintilla;
    public float directionX;
    public float directionY;
    public Color coloreBall;


    public bool PrimoInnesto;
    public bool Innestabile = true;
    public bool InseritaNelCircuito = false;

    public GameObject TeleportDestinazione;
    
    private Animator animazione;

    private bool isZooming = false;
    private float ZoomSpeed = 30f;
    private float ZoomEndScale = 0.8f;



    private float beforePauseX;
    private float beforePauseY;
    private float beforePauseSpeed;


    // Start is called as you start the game, we use it to initially give values to things

    private void Awake()
    {
        animazione = GetComponent<Animator>();
        PrimoInnesto = true;
    }

    void Start()
    {

        speed = Main.Level.LevelSpeedPerimetro;


        //VERIFICO SE E' LA PRIMA VOLTA CHE SBUCA LA PALLA SPECIALE
        if (DescrizioneFunzione != "" && Main.Tutorial.CandidateToShow(transform.tag))
        {
            SetPauseBall(true);
            Main.GUI.DoCinematicOnObject(transform.gameObject, DescrizioneFunzione);
        }
    }

    void FixedUpdate()
    {
        if (!Main.GamePaused)
        {
            if (speed > 0)
            {
                // Here we assign those values to a Vector2 variable.
 //               Debug.Log(transform.name + " --> DirectionX=" + directionX);
                Vector2 movement = new Vector2(transform.position.x + (speed * Time.fixedDeltaTime * directionX), transform.position.y + (speed * Time.fixedDeltaTime * directionY));

                transform.position = movement;
            }

            if (isZooming)
            {

                Vector3 tmp = new Vector3(transform.localScale.x - ((1 - ZoomEndScale) / ZoomSpeed), transform.localScale.y - ((1 - ZoomEndScale) / ZoomSpeed), 1);

                transform.localScale = tmp;

                if (tmp.x <= ZoomEndScale)
                {
                    transform.localScale = new Vector3(ZoomEndScale, ZoomEndScale, 1);

                    isZooming = false;

                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject go = coll.gameObject;

        if (go.CompareTag("Muro"))
        {
            ResumeBallMovement(-directionX, -directionY, speed);
        }

        if (go.CompareTag("ResetBall"))
        {
            Innestabile = true;
            TeleportDestinazione = null;
        }



        

    }


    public void StopBall()
    {

        speed = 0;
        directionX = 0;
        directionY = 0;

        animazione.speed = 0;
    }

    public void SetPauseBall(bool stato)
    {
        if (stato)
        {
            beforePauseX = directionX;
            beforePauseY = directionY;
            if (speed > 0) { beforePauseSpeed = speed; }
            StopBall();
        }
        else
        {
            ResumeBallMovement(beforePauseX, beforePauseY, beforePauseSpeed);
        }
    }



    public void ResumeBallMovement(float _x, float _y, float _speed)
    {
        
        directionX = _x;
        directionY = _y;
        speed = _speed;


        if ( _x == 1 || _y == 1)
        {
            animazione.SetFloat("DirezionePalla",-1);

        }

        if (_x == -1 || _y == -1)
        {
            animazione.SetFloat("DirezionePalla", 1); ;
        }

        //Ora ruoto la palla per sicurezza
        Quaternion rotazionepalla = new Quaternion();
        if (_x == 1)
        {
            rotazionepalla = Quaternion.Euler (0, 0, 0);
        }
        else if (_x == -1)
        {
            rotazionepalla = Quaternion.Euler (0, 0, 180);
        }
        else if (_y == -1)
        {
            rotazionepalla = Quaternion.Euler (0, 0, 270);
        }
        else if (_y == 1)
        {
            rotazionepalla = Quaternion.Euler (0, 0, 90);
        }
        //****************************
        //Debug.Log("X: " + _x + " Y: " + _y);


        transform.localRotation = rotazionepalla;
        transform.localScale = new Vector3(1.0f, 1.0f,1f);
        transform.parent = null;
        animazione.speed = 1;

    }

    public void ZoomBall()
    {
        isZooming = true;
        
    }

    public void ScattaScintilla()
    {
        Vector2 newpos = new Vector2(transform.position.x, transform.position.y + 2);
        Instantiate(Scintilla, newpos, transform.rotation);
        Scintilla.SetActive(true);
        Main.Audio.PlaySound(Main.Audio.Suoni.LoculoPieno);
    }
    
}
