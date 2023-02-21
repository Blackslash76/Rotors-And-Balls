using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClosedPanel_Manager : MonoBehaviour
{
    public GameObject PannelloChiuso;
    public TextMeshProUGUI TestoStelleRichieste;
    public GameObject Stellina;
    public int StelleRichiestePerSblocco=100;
    // Start is called before the first frame update
    void Start()
    {
        if (Main.StarsCollected < StelleRichiestePerSblocco)
        {
            Stellina.GetComponent<Animator>().Play("Ginny_Riscaldamento");
            TestoStelleRichieste.text = Main.StarsCollected + " / " + StelleRichiestePerSblocco;

        }
        else
        {
            PannelloChiuso.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
