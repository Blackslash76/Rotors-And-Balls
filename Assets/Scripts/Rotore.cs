using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotore : MonoBehaviour {

    public enum TipoRotore { Normale, Multicolore}
    public TipoRotore tipoRotore = TipoRotore.Normale;

    const int SENSO_ORARIO = 0;
    const int SENSO_ANTIORARIO = 1;
    public int HitPoints = 1;

    const int numInnestiRotore = 4;
    private float TimeToAddWhenCompletingRotor = 0f;

    private string[] PalleInnestate = new string[numInnestiRotore];
    private string[] PalleRichieste = new string[numInnestiRotore];



    public bool IsRotating = false;
    public bool isClosing = false;
    public bool NewBallIncoming = false;

    private Rigidbody2D rb2D;
    private float rotSpeed = 800f;

    private float RotazioneGradiFine = 0f;
    private int RotazioneDirezione;

    private Animator anim;
    private LevelManager levelManager;


    // Use this for initialization

    private void Awake()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        anim = transform.parent.Find("AnelloChiusure").GetComponent<Animator>();
        levelManager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
    }

    private void FixedUpdate()
    {
        if (IsRotating)
        {
            if (RotazioneDirezione == SENSO_ANTIORARIO)
            {
                rb2D.MoveRotation(rb2D.rotation + rotSpeed * Time.fixedDeltaTime);
                if (rb2D.rotation >= RotazioneGradiFine)
                {
                    Debug.Log("Rotazione attuale: " + rb2D.rotation + " - Rotazione Dest: " + RotazioneGradiFine);
                    IsRotating = false;
                    rb2D.rotation = RotazioneGradiFine;
                    FineRotazione();
                }
            }
            else
            {
                rb2D.MoveRotation(rb2D.rotation - rotSpeed * Time.fixedDeltaTime);
                if (rb2D.rotation <= RotazioneGradiFine)
                {
                    //Debug.Log("Rotazione attuale: " + rb2D.rotation + " - Rotazione Dest: " + RotazioneGradiFine);
                    IsRotating = false;
                    rb2D.rotation = RotazioneGradiFine;
                    FineRotazione();
                }
            }
        }

        if (isClosing)
        {
            //Verifico Stato Animazione
            
            //if (!anim.GetBool("Closing"))
            
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >=1 )
            {
                SvuotaCaricatore();
                isClosing = false;
                anim.Play("Idle");
                transform.parent.Find("AnelloChiusure").gameObject.SetActive(false);


                //**********************************************
                //ASSEGNO I BENEFIT
                Main.Player.AddScore(1000);
                Main.IncreaseSeriesComplete();

                //Assegno le monete
                if (Main.Level.LevelsStatusCompleted[Main.Level.LevelNumber] > -1)
                {
                    int monetemin = Main.Level.LevelDifficulty + 1;
                    int monetemax = Random.Range(
                        monetemin + 1, monetemin + ((Main.Level.LevelDifficulty + 1) * 3)
                        );
                    levelManager.SpawnRandomCoin(
                        Random.Range(monetemin, monetemax) * Main.Level.LevelMoneyMultiplier, 
                        transform.position.x, transform.position.y
                        );
                }

                //**********************************************


                //Accendo la lampadina se gli hitpoints sono minori o uguali a zero
                if (HitPoints <= 0)
                {
                    GameObject Lampadina = transform.parent.Find("RotoreTop/ParticleLampadina").gameObject;
                    Lampadina.SetActive(true);
                    //Lampadina.transform.GetComponent<ParticleSystem>().startColor=

                    if (HitPoints == 0)
                    {
                        //Aggiungo del tempo
                        Main.Level.AddExtraTime(TimeToAddWhenCompletingRotor);
                    }
                    else
                    {
                        //Se il rotore è già completo aggiungo un terzo del tempo
                        Main.Level.AddExtraTime(TimeToAddWhenCompletingRotor / 3);
                    }

                }
            }
        }

    }

    void Start () {
        int i = 0;
        foreach (string s in PalleInnestate)
        {
            PalleInnestate[i] = "";
            PalleRichieste[i] = "";
            i += 1;
        }
    }

    private bool UpdateData()
    {
        int i = 0;
        bool retvalue = true;

        foreach (Component child in GetComponentsInChildren<ScriptInnesto>())
        {
            Component loculo = child.GetComponentInChildren<BallManager>();

            if (loculo)
            {
                PalleInnestate[i] = loculo.transform.tag;
                if (tipoRotore==TipoRotore.Multicolore)
                {
                    Spicchio spicchio = child.GetComponentInChildren<Spicchio>();
                    PalleRichieste[i] = spicchio.PallaAmmessa;
                }
            }
            else
            {
                PalleInnestate[i] = "";
                PalleRichieste[i] = "";
                retvalue = false;
            }

            i++;
        }
         
        return retvalue;
    }
    public void VerificaCaricatore()
    {
        int Found = 0;
        bool step1check;
        string seedTag="";
         
        //Verifico che il caricatore sia tutto pieno sennò inutile sprecare risorse
        step1check = this.UpdateData();

        if (step1check)
        {
                for (int i = 0; i < PalleInnestate.Length; i++)
                {
                //Se è una Pokeball passa il turno
                if (PalleInnestate[i] == "Ball,Pokeball")
                {
                    Found += 1;
                }
                else
                {
                    //LOGICA PER ROTORE DI TIPO NORMALE
                    if (tipoRotore == TipoRotore.Normale)
                    {

                        //Se è la prima palla colorata che trovo usala come modello e passa il turno
                        if (seedTag == "")
                        {
                            seedTag = PalleInnestate[i];
                            Found += 1;
                        }
                        else
                        {
                            //Se abbiamo già il modello controlla il resto delle palle con esso per vedere se passano il turno
                            if (PalleInnestate[i] == seedTag)
                            {
                                Found += 1;
                            }
                        }
                    }
                    //LOGICA PER ROTORE MULTIBALL
                    if (tipoRotore == TipoRotore.Multicolore)
                    {
                        if (PalleInnestate[i] == PalleRichieste[i])
                        {
                            Found += 1;
                        }
                    }
                }
            }
            

            //Verifico che un colore abbia raggiunto un numero uguale agli innesti (tutte le palline uguali) e allora attivo l'animazione di chiusura dei loculi
            if (Found == PalleInnestate.Length)
            {
                transform.parent.Find("AnelloChiusure").gameObject.SetActive(true);
                anim.Play("AnelloChiusura_Chiusura");
                Main.Audio.PlaySound(Main.Audio.Suoni.RotoreCompleto);
                isClosing = true;
            }
        }
    }

    void SvuotaCaricatore()
    {
        //Distruggi le palle
        int total = this.transform.childCount;

        foreach (Transform obj in GetComponentInChildren<Transform>())
        {
            if (obj.tag == ("Innesto"))
            {
                Destroy(obj.GetComponentInChildren<BallManager>().gameObject);
                obj.GetComponent<ScriptInnesto>().SlotOccupato = false;
            }
        }

        HitPoints -= 1;
    
    }

    public int BallsInnestateConTag(string tag)
    {

        int num=0;
        
        foreach(string s in PalleInnestate)
        {
            Debug.Log(s);
            if (s == tag) { num += 1; }
        }
        return num;
    }

    public void TrasformaPallaCasuale(GameObject PallaSource)
    {

    }

    public void AvviaRotazione(int direzione)
    {
        if (direzione == SENSO_ANTIORARIO) //antiorario
        {
            RotazioneGradiFine = (rb2D.rotation + 90f);
        }
        else
        {
            RotazioneGradiFine = (rb2D.rotation - 90f);
        }

        RotazioneDirezione = direzione;
        IsRotating = true;
        //Debug.Log("***Start Rotating***");
        //Suono
        Main.Audio.PlaySound(Main.Audio.Suoni.RotazioneRotore);

        if (tipoRotore == TipoRotore.Multicolore)
        {
            Animator rotore_anim = transform.parent.GetComponent<Animator>();
            rotore_anim.speed = 0;
        }

    }

    private void FineRotazione()
    {

        // Aggiorno la variabile "posizione" di ogni innesto, tornerà utile per definire lo sblocco delle balls
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Innesto")
            {

                ScriptInnesto innestoScript = transform.GetChild(i).GetComponent<ScriptInnesto>();

                if (RotazioneDirezione == SENSO_ANTIORARIO)
                {
                    innestoScript.posizione = (innestoScript.posizione + 1) % 4;
                }
                else
                {
                    innestoScript.posizione = (innestoScript.posizione - 1);
                    if (innestoScript.posizione == -1)
                    {
                        innestoScript.posizione = 3;
                    }
                }

            }
        }
        if (tipoRotore == TipoRotore.Multicolore)
        {
            Animator rotore_anim = transform.parent.GetComponent<Animator>();
            rotore_anim.speed = 1;
        }

    }


}
