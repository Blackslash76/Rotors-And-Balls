using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineDiscoLampadina : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = transform.GetComponent<Animator>();
        //anim.Play("StartLineDiscoCorpo_Lampadina");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MassAreaPalline"))
        {
            anim.Play("StartLineDiscoCorpo_AccendiLampadina");
            //anim.Play("StartLineDiscoCorpo_Lampadina");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MassAreaPalline"))
        {
            //anim.SetFloat("Direzione", 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MassAreaPalline"))
        {
            anim.Play("StartLineDiscoCorpo_SpegniLampadina");
        }
    }
}
