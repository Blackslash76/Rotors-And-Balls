using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_AnimManager : MonoBehaviour
{
    private float Direzione;
    private int Spostamento = -12;
    private float speed = 0.5f;

    private List<GameObject> Teleports_StessoGruppo = new List<GameObject>();
    private Transform CapsulaA, CapsulaB;

    [SerializeField]
    private AudioClip TeleportChargeSound;


    // Start is called before the first frame update
    void Awake()
    {
        
        Teleporter.Gruppo gruppo = transform.Find("Basetta").GetComponent<Teleporter>().gruppo;

        foreach (GameObject tp in GameObject.FindGameObjectsWithTag("Teleporter"))
        {
            if (tp.GetComponent<Teleporter>().gruppo == gruppo)
            {
                Teleports_StessoGruppo.Add(tp.transform.parent.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (GameObject tp in Teleports_StessoGruppo)
        //{
        //    CapsulaA = tp.transform.Find("CorpoA");
        //    CapsulaB = tp.transform.Find("CorpoB");
        CapsulaA = transform.Find("CorpoA");
        CapsulaB = transform.Find("CorpoB");
        //Muoviamo le capsule affinchè si aprano e chiudano

        if (Direzione == -1 && CapsulaA.localPosition.x > Spostamento)
            {
//                Debug.Log("TP: " + tp.name + "--CapsulaA: " + CapsulaA.localPosition.x + "--CapsulaB: " + CapsulaB.localPosition.x);
                CapsulaA.Translate(-speed, 0, 0);
                CapsulaB.Translate(-speed, 0, 0);
            }

            if (Direzione == 1 && CapsulaA.localPosition.x < -4)
            {
//                Debug.Log("TP: " + tp.name + "--CapsulaA: " + CapsulaA.localPosition.x + "--CapsulaB: " + CapsulaB.localPosition.x);
                CapsulaA.Translate(speed, 0, 0);
                CapsulaB.Translate(speed, 0, 0);
            }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Ball,"))
        {
            Direzione = -1;

            Main.Audio.PlaySoundFX(TeleportChargeSound, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Ball,"))
        {
            Direzione = 1;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains("Ball,"))
        {
            Direzione = -1;
        }
    }
}
