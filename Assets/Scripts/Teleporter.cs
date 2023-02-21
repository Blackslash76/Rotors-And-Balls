using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public int Teleport_Order_InGroup = 0;

    [HideInInspector]
    public bool destination = false;

    public enum Gruppo {Gruppo1, Gruppo2, Gruppo3, Gruppo4, Gruppo5};
    public Gruppo gruppo;

    [SerializeField]
    private bool randomDestination = false;

    private float DirezioneX;
    private float DirezioneY;

    private List<GameObject> Teleports_StessoGruppo = new List<GameObject>();
    private GameObject NextTeleport;
    private Animator Bagliore;

    [SerializeField]
    private AudioClip TeleportFireSound;

    private void Awake()
    {
        foreach (GameObject tp in GameObject.FindGameObjectsWithTag("Teleporter"))
        {
            if (tp.GetComponent<Teleporter>().gruppo == gruppo)
            {
                Teleports_StessoGruppo.Add(tp);
            }
        }

        Bagliore = transform.Find("Bagliore").GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject BallDaTrasferire;


        if (collision.tag.Contains("Ball,") )
        {
            BallDaTrasferire = collision.gameObject;
            destination = BallDaTrasferire.GetComponent<BallManager>().TeleportDestinazione != null;
            if (!destination)
            {
                int nextnum;

                if (!randomDestination)
                {
                    foreach (GameObject go in Teleports_StessoGruppo)
                    {
                        nextnum = go.GetComponent<Teleporter>().Teleport_Order_InGroup;

                        if (nextnum == (Teleport_Order_InGroup + 1) % Teleports_StessoGruppo.Count)
                        {
                            NextTeleport = go;
                            break;
                        }
                    }
                }
                else
                {
                    do
                    {
                        nextnum = Random.Range(0, Teleports_StessoGruppo.Count);
                    }
                    while (nextnum != Teleport_Order_InGroup);
                    
                    NextTeleport = Teleports_StessoGruppo[nextnum];
                }
                if (NextTeleport)
                {
                    //PUFF
                    Debug.Log("Next TP:" + NextTeleport.name);
                    BallDaTrasferire.GetComponent<BallManager>().StopBall();
                    BallDaTrasferire.GetComponent<BallManager>().TeleportDestinazione = NextTeleport;
                    BallDaTrasferire.transform.position = transform.position; //Questa mi serve per allineare la palla sul teletrasporto
                    Bagliore.Play("BaglioreTeletrasporto", 0, 0);
                    Main.Audio.PlaySoundFX(TeleportFireSound, 1f);

                    StartCoroutine(WaitAndMoveBallToDestination(0.2f, BallDaTrasferire));
                }
                
            }
            else
            {
                //E' un Teletrasporto di destinazione

                //PUFF
                Bagliore.Play("BaglioreTeletrasporto", 0, 0);
                Main.Audio.PlaySoundFX(TeleportFireSound, 0.75f);

                RicalcolaDirezioneXeY();
                BallDaTrasferire.transform.GetComponent<BallManager>().ResumeBallMovement(DirezioneX, DirezioneY, Main.Level.LevelSpeedCircuito);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Ball,"))
        {
            destination = false;
        }
    }

    private void RicalcolaDirezioneXeY()
    {
        switch (Mathf.RoundToInt(transform.eulerAngles.z))
        {
            case 0:
                {
                    DirezioneX = 0;
                    DirezioneY = -1;
                }
                break;
            case 90:
                {
                    DirezioneX = 1;
                    DirezioneY = 0;
                }
                break;
            case 180:
                {
                    DirezioneX = 0;
                    DirezioneY = 1;
                }
                break;
            case 270:
                {
                    DirezioneX = -1;
                    DirezioneY = 0;
                }
                break;
        }

    }

    IEnumerator WaitAndMoveBallToDestination(float waitTime, GameObject BallDaTrasferire)
    {

        yield return new WaitForSeconds(waitTime);
        BallDaTrasferire.transform.position = BallDaTrasferire.GetComponent<BallManager>().TeleportDestinazione.transform.position;
    }
}
