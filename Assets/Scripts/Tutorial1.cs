using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class Tutorial1 : MonoBehaviour {
    private string[] testo;

    public int step;

    public TextMeshProUGUI testopannello;
    public GameObject Powerball_Fase1;
    public GameObject Rotore_Fase1;
    public GameObject ballSpawner_Fase1;
    public GameObject Scene_Fase2;

    public GameObject AnelloLoculi1_Fase2;
    public GameObject AnelloLoculi2_Fase2;


    //Semafori
    private bool WaitingForPrimoInnesto= false;
    private bool WaitingForPrimaRotazione = false;
    // Use this for initialization
    void Awake () {


        testo = new string[11];
        testo[0] = "CIAO, BENVENUTO NELLA CENTRALE ENERGETICA WONDERFUEL, QUI PRODUCIAMO L'ENERGIA NECESSARIA " +
                   "AL FUNZIONAMENTO DELLA CITTA' GRAZIE A DELLE SFERE SPECIALI CHIAMATE *POWERBALL*";
        testo[1] = "QUESTA E' UNA *POWERBALL*, NE TROVERAI DI DIVERSO TIPO SULLA TUA STRADA, QUESTA IN PARTICOLARE E' UNA *POWERBALL* DI TIPO <ACQUA> DI COLORE BLU";
        testo[2] = "QUESTO MECCANISMO INVECE SI CHIAMA <ROTORE> E HA LA CAPACITA' DI ESTRARRE L'ENERGIA DELLE *POWERBALLs*, A PATTO CHE SU TUTTI E 4 I LATI CI SIANO" +
                    "*POWERBALLS* DELLO STESSO COLORE";
        testo[3] = "LE *POWERBALLS* ROTOLANO VERSO I ROTORI, INNESTANDOSI NEL PRIMO LIBERO";
        testo[4] = "IL NOSTRO SCOPO E' DARE ENERGIA COMPLETA AL ROTORE CON 4 POWERBALLS DELLO STESSO COLORE, PUOI RUOTARE I ROTORI TOCCANDOLI " +
            "SUL ROTORE. PROVA!!!";
        testo[5] = "OTTIMO!! ORA COMPLETA IL ROTORE CON ALTRE TRE PALLINE BLU PER PRODURRE UNA SFERA DI ENERGIA";
        testo[6] = "BENE! IMPARI IN FRETTA! COME VEDI ORA IL ROTORE E' ACCESO, IL NOSTRO COMPITO E' ACCENDERE TUTTI I ROTORI!";
        testo[7] = "E' POSSIBILE SPOSTARE LE POWERBALLS UTILIZZANDO I TUBI DI COLLEGAMENTO TRA I ROTORI";
        testo[8] = "BASTA TRASCINARE IL DITO SU UNA POWERBALL INNESTATA NEL ROTORE PER FARLA PARTIRE. PROVA!";
        testo[9] = "BENISSIMO! ORA COMPLETA QUESTI DUE ROTORI SCAMBIANDO LE POWERBALLS TRA DI LORO";
        testo[10] = "GRANDIOSO! RICORDA CHE I ROTORI VANNO COMPLETATI ENTRO UN TEMPO LIMITE. BUON LAVORO!";
    }

    private void Start()
    {
        Main.Tutorial.itsTutorial = true;
        TutorialGoToStep(step);
    }

    private void Update()
    {
        if (WaitingForPrimoInnesto)
        {
            //Controllo se la variabile "Primo Innesto" della ball sia false, in tal caso la palla è innestata
            if (!Powerball_Fase1.GetComponent<BallManager>().PrimoInnesto)
            {
                WaitingForPrimoInnesto = false;
                step += 1;
                TutorialGoToStep(step);

            }
        }

        if (WaitingForPrimaRotazione)
        {
            if (Rotore_Fase1.transform.Find("AnelloLoculi").transform.GetComponent<Rotore>().IsRotating)
            {
                WaitingForPrimaRotazione = false;
                step += 1;
                TutorialGoToStep(6);
            }
            //Devo verificare se il rotore è stato girato
        }

        if (step==7) //Attesa che l'utente completi il rotore
        {
            if (Rotore_Fase1.transform.Find("AnelloLoculi").transform.GetComponent<Rotore>().HitPoints == 0)
            {
                
                testopannello.text = testo[6];
                ballSpawner_Fase1.SetActive(false);
                Main.GamePaused = true;
                Main.InputStopped = true;
                step += 1;
                }

        }

        if (step==10) // Attesa che l'utente sganci la sua prima powerball nel tubo
        {
            if (AnelloLoculi1_Fase2.GetComponentsInChildren<BallManager>().Length<4 || AnelloLoculi2_Fase2.GetComponentsInChildren<BallManager>().Length < 4)
            {
                testopannello.text = testo[9];
                step += 1;
            }
        }

        if (step==11) // Attesa completamento dei rotori
        {
            if (AnelloLoculi1_Fase2.GetComponent<Rotore>().HitPoints==0 && AnelloLoculi2_Fase2.GetComponent<Rotore>().HitPoints == 0)
            {
                testopannello.text = testo[10];
                step += 1;
            }
        }
    }

    public void TutorialGoToStep(int _step)
    {
        switch (_step)
        {
            case 0:
                {

                    //Primo step visualizzo il testo
                    testopannello.text = testo[0];
                    break;
                }
            case 4:
                {
                    //Dopo che si è innestata ora aspettiamo la prima rotazione
                    testopannello.text = testo[4];
                    WaitingForPrimaRotazione = true;
                    Main.InputStopped = false;
                    step += 1;
                    break;
                }
            case 6:
                {
                    //Ora aspetto che l'utente completi un rotore;
                    ballSpawner_Fase1.SetActive(true);
                    ballSpawner_Fase1.transform.GetComponent<RandomSpawn>().SpawnBall();
                    testopannello.text = testo[5];
                    step += 1;
                    break;
                }
        }
    }

    public void PannelloHelp_CLICK()
    {
        if (step == 0)
        {

            //Spawno la powerball
            Main.InputStopped = true;
            Powerball_Fase1.SetActive(true);
            testopannello.text = testo[1];
            Main.Level.MainCamera.Zooma(15, 2f, Powerball_Fase1.transform.position.x, Powerball_Fase1.transform.position.y);
            Powerball_Fase1.GetComponent<BallManager>().SetPauseBall(true);
            step += 1;
        }
        else if (step == 1)
        {
            //Spawno il Rotore
            Rotore_Fase1.SetActive(true);
            testopannello.text = testo[2];
            Main.Level.MainCamera.Zooma(15, 2f, Rotore_Fase1.transform.position.x, Rotore_Fase1.transform.position.y);
            step += 1;
        }
        else if (step == 2)
        {
            //Ora faccio ruotare la palla verso il rotore
            Main.PauseGame(false);
            testopannello.text = testo[3];
            Main.Level.MainCamera.ResetZoom();
            Powerball_Fase1.GetComponent<BallManager>().ResumeBallMovement(1, 0, 20);
            WaitingForPrimoInnesto = true;
            step += 1;

        }
        else if (step == 8)
        {
            //Fine Prima Parte Tutorial, inizio della prossima
            Main.Level.MainCamera.transform.position = new Vector3(Scene_Fase2.transform.position.x, Scene_Fase2.transform.position.y, Main.Level.MainCamera.transform.position.z);
            testopannello.text = testo[7];
            step += 1;
        }
        else if (step == 9)
        {
            testopannello.text = testo[8];
            Main.GamePaused = false;
            Main.InputStopped = false;
            step += 1;
        }
        else if (step== 12) //LAST STEP
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Caricatore"))
            {
                go.GetComponent<Rotore>().HitPoints = 0;
                
            }
            SceneManager.LoadScene(Main.MainMenuScene);
        }

    }

    private void OnDestroy()
    {
        Main.Tutorial.itsTutorial = false;
    }

}
