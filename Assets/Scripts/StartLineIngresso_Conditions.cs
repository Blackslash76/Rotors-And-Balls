using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineIngresso_Conditions : MonoBehaviour
{
    private ScriptInnesto innesto;
    private Rotore rotore;
    private bool PiattaformaTuboScambio = false;
    private bool FrecciaTuboScambio = false;

    private void OnTriggerStay2D(Collider2D collision)
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

        if (collision.transform.CompareTag("Freccia"))
        {
            FrecciaTuboScambio = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Caricatore"))
        {
            rotore = null;
        }

        if (collision.transform.CompareTag("Innesto"))
        {
            innesto = null;
        }

        if (collision.transform.CompareTag("TuboScambio,Piattaforma"))
        {
            PiattaformaTuboScambio = false;
        }

        if (collision.transform.CompareTag("Freccia"))
        {
            FrecciaTuboScambio = false;
        }

    }

    public bool VerificaVarcoAttivo(Transform collision)
    {
        BallManager ball = collision.GetComponent<BallManager>();

        if (rotore && innesto)
        {
            if (!rotore.IsRotating && !innesto.SlotOccupato && ball.PrimoInnesto)
            {
                rotore.NewBallIncoming = true;
                return true;
            }
            else
            {
                //Debug.Log("Rotore:" + rotore.IsRotating + " - Innesto:" + innesto.SlotOccupato + " - Ball:" + ball.PrimoInnesto);
                return false;
            }
        }
        else if (PiattaformaTuboScambio && !FrecciaTuboScambio)
        {
            return true;
        }
        else
        {
            Debug.Log("Rotore rilevato:" + rotore + " ** Innesto rilevato:" + innesto);
            return false;
        }
    }
}

