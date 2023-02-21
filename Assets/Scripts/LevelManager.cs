using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float velocitàPallaSuPerimetro = 0;
    public float velocitàPallaSuCircuito = 0;
    public float tempoLimiteLivello;
    public float tempoLimitePerPalla = 0;
    public int Level_BaseEnergyCost = 5;
    public string levelDescription;
    public int levelNumberInWorld = -1;

    private float timestart;
    private float[] moltiplicatoreTempoRotore = new float[3] { 60f, 30f, 18f };
    

    private SimpleHealthBar healthBar;

    public GameObject coin;

    bool EndOfLevel = false;

    private bool HelpShowed = false;


    private void Awake()
    {

        healthBar = GameObject.FindObjectOfType<SimpleHealthBar>();

        RemoveBorderFromObjects();

        //Modifico in modo empirico i costi del livello
        Level_BaseEnergyCost = 2;

        //Salvo i dati correnti
        SaveLoad.Save();
    }

    private void Start()
    {
        //Se time limit = -1 nell'editor allora calcolo automaticamente il tempo
        if (tempoLimiteLivello == -1)
        {
            
            GameObject[] rotors = GameObject.FindGameObjectsWithTag("Caricatore");
            foreach (GameObject go in rotors)
            {
                tempoLimiteLivello += go.GetComponent<Rotore>().HitPoints * moltiplicatoreTempoRotore[Main.Level.LevelDifficulty];
            }
        }
        Main.Level.InitLevel(levelNumberInWorld,tempoLimiteLivello, velocitàPallaSuCircuito*1.5f,velocitàPallaSuPerimetro*1.5f);
        Main.PauseGame(true);

        if (levelNumberInWorld > -1)
        {
            Main.Level.MainCamera.EnablePreviewCamera();
            Main.GUI.ShowStartLevelUI(true);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!Main.GamePaused)
        {
            this.EndOfLevelCheck();
            if (Main.Level.TimeLeft >=0)
            {
                healthBar.UpdateBar(Main.Level.TimeLeft, Main.Level.LevelCurrentTime);
                
                if (Main.Level.TimeLeft == (Main.Level.LevelCurrentTime / 4) && !HelpShowed)
                {
                    Main.PauseGame(true);
                    Main.GUI.ShowNeedMoreEnergyPopup();
                    HelpShowed = true;
                }
            }
            else
            {
                Main.PauseGame(true);
                Main.GUI.ShowGameOverUI(true);
            }
        }
	}

    void EndOfLevelCheck()
    {
        foreach (GameObject go in Main.Level.RotoriLivello)
        {
            if (go.GetComponent<Rotore>().HitPoints <= 0)
            {
                EndOfLevel = true;
            }
            else
            {
                EndOfLevel = false;
                break;
            }
        }

        if (EndOfLevel)
        {
            //Finito il livello, carico il prossimo
            Debug.Log("Level complete " + Main.Level.TimeLeft);
            Main.PauseGame(true);

            Main.GUI.ShowEndOfLevelUI(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            Destroy(collision.gameObject);
            Main.Player.AddSubCoins(1);
            GameObject.FindObjectOfType<Coins_GUI>().PlayAnimation();
        }
    }

    public void SpawnRandomCoin(int _quantity, float _startPointX, float _startPointY)
    {

        for (int i=0; i < _quantity;i++)
        {
            Vector2 whereToSpawn = new Vector2(_startPointX + Random.Range(-20f,20f), _startPointY + Random.Range(-20f, 20f));

            Instantiate(coin, whereToSpawn, Quaternion.identity);
        }
    }

    private void RemoveBorderFromObjects()
    {
        foreach (GameObject border in GameObject.FindGameObjectsWithTag("Cornice"))
        {
            border.SetActive(false);
        }
    }

    
}
