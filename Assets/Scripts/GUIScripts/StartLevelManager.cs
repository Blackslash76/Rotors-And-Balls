using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using EnergySuite;

public class StartLevelManager : MonoBehaviour {

    public Button startGameButton;
    public Button mainMenuButton;
    public TextMeshProUGUI levelTitle;
    public TextMeshProUGUI levelDescription;
    public Text costText;

    public Button needEnergyButton;
    public TextMeshProUGUI EnergyAmount_Text;
    public TextMeshProUGUI EnergyTimeLeft_Text;

    public Image PlayerIcon;
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerScoreText;

    public Image InternetPlayerIcon;
    public TextMeshProUGUI InternetPlayerName;
    public TextMeshProUGUI InternetScoreText;

    public GameObject CrownIcon;
    public TextMeshProUGUI DoubleMoneyText;
    
    public Canvas ContentCanvas;

    private LevelManager levelManager;
    private IconsList iconslist;
    
    private string[] LevelDifficulty = { "EASY", "NORMAL", "HARD" };

    private int StartIcon = Main.Level.InternetScores.Icona;

    private int levelcost;

    private void Awake()
    {
        EnergySuiteManager.OnAmountChanged += EnergyAmountChanged;
        EnergySuiteManager.OnTimeLeftChanged += OnTimeLeftChanged;

        levelManager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
        iconslist = GameObject.FindObjectOfType<IconsList>();

        //COSTO IN ENERGIA IN BASE ALLA DIFFICOLTA'
        if (Main.Level.LevelDifficulty == 0)
        {
            if (Main.Level.LevelsStatusCompleted[Main.Level.LevelNumber] == -1)
            {
                // SE NON HO MAI COMPLETATO IL LIVELLO FALLO COSTARE 1
                levelcost = 1;
            }
            else
            {
                levelcost = levelManager.Level_BaseEnergyCost;
            }
        }
        else if (Main.Level.LevelDifficulty == 1)
        {
            levelcost = levelManager.Level_BaseEnergyCost + 2;
        }
        else if (Main.Level.LevelDifficulty == 2)
        {
            levelcost = levelManager.Level_BaseEnergyCost + 3;
        }
    }
    
    private void Start()
    {
        //STOPPO IL TEMPO
        //Main.PauseGame(true);
        StartGameButton_ChangeStatus(EnergySuiteManager.GetAmount(TimeValue.Energy));


        //CAMBIO I DATI RELATIVI AL LIVELLO
        levelTitle.text = "LEVEL " + levelManager.levelNumberInWorld + " - " + LevelDifficulty[Main.Level.LevelDifficulty];
        levelDescription.text = levelManager.levelDescription;

        //CAMBIO I DATI RELATIVI AL PLAYER
        PlayerIcon.sprite = iconslist.IconsProfile[Main.Player.PlayerIcon];

        PlayerScoreText.text = "YOU - " + Main.Level.Scores[levelManager.levelNumberInWorld, Main.Level.LevelDifficulty];

        //ANCHE IL COSTO IN ENERGIA
        costText.text= levelcost.ToString(new string('0', 2));

        //FACCIO IL FADE
        StartCoroutine(FadeEffect.FadeCanvas(ContentCanvas.GetComponent<CanvasGroup>(), 0f, 1f, 1f));

        //SUONO KNUCKLE JOE
        Main.Audio.PlaySound(Main.Audio.Suoni.StartNewLevel);

        //Azzero il tempo
        Main.Level.InitLevelTimer(Main.Level.LevelCurrentTime);

        //Inizializzo lo spawner
        SpawnerData.UpdateLevelData();


    }


    private void Update()
    {

        StartGameButton_ChangeStatus(EnergySuiteManager.GetAmount(TimeValue.Energy));
        
        //Aggiorno Lo Score da Internet
        InternetScoreText.text = Main.Level.InternetScores.Nome + " - " + Main.Level.InternetScores.Punteggio;
        InternetPlayerIcon.sprite = iconslist.IconsProfile[Main.Level.InternetScores.Icona];
        StartIcon = Main.Level.InternetScores.Icona;

        if (Main.Level.InternetScores.Guid == Main.UID)
        {
            PlayerScoreText.text = "CONGRATULATIONS! YOU ARE THE LEVEL KING!";
            DoubleMoneyText.text = "DOUBLE MONEY !!!";
            CrownIcon.SetActive(true);
            Main.Level.LevelMoneyMultiplier = 2;
        }
        else
        {
            PlayerScoreText.text = "YOU - " + Main.Level.Scores[levelManager.levelNumberInWorld, Main.Level.LevelDifficulty];
            DoubleMoneyText.text = "";
            CrownIcon.SetActive(false);
            Main.Level.LevelMoneyMultiplier = 1;
        }

        // Energy
        needEnergyButton.interactable = Main.ConnessoAInternet;        

    }

    #region Eventi Buttons
    public void StartGameButton_OnClick()
    {
        //if (!Debug.isDebugBuild)
        //{
        //    EnergySuiteManager.Use(TimeValue.Energy, levelManager.Level_BaseEnergyCost);
        //}

        EnergySuiteManager.Use(TimeValue.Energy, levelcost);


        this.gameObject.SetActive(false);
        Main.Level.MainCamera.DisablePreviewCamera();
        
        //Tolgo la pausa
        Main.PauseGame(false);

        //Spawno la palla
        SpawnerData.StartSpawner();

        //Start Music
        Main.Audio.PlaySound(Main.Audio.Suoni.MusicaGioco);
    }

    private void EnergyAmountChanged(int amount, TimeBasedValue timeBasedValue)
    {
        StartGameButton_ChangeStatus(amount);

    }

    void OnTimeLeftChanged(System.TimeSpan timeLeft, TimeBasedValue timeBasedValue)
    {
        if (EnergySuiteManager.GetAmount(TimeValue.Energy) == EnergySuiteManager.GetMaxAmount(TimeValue.Energy))
        {
            EnergyTimeLeft_Text.text = "";
        }
        else
        {
            string formatString = string.Format("{0:00}:{1:00}", timeLeft.Minutes, timeLeft.Seconds);
            EnergyTimeLeft_Text.text = formatString;
        }
    }
    private void StartGameButton_ChangeStatus(int amount)
    {
        try
        {
            EnergyAmount_Text.text = EnergySuiteManager.GetAmount(TimeValue.Energy).ToString(new string('0', 2)) + "/" + EnergySuiteManager.GetMaxAmount(TimeValue.Energy);
                                    startGameButton.interactable = (amount >= levelcost);
        }
        catch { }

        try
        {
            needEnergyButton.gameObject.SetActive(
                Main.MoneyMoney.ADReady && 
                !(EnergySuiteManager.GetAmount(TimeValue.Energy) == EnergySuiteManager.GetMaxAmount(TimeValue.Energy)));

        }
        catch { }
    }

    public void NeedEnergyButton_OnClick()
    {
        Main.MoneyMoney.MostraPubblicitàEnergy(EnergySuiteManager.GetMaxAmount(TimeValue.Energy));
        startGameButton.interactable = true;
    }

    public void MainMenuButton_OnClick()
    {
        //Main.PauseGame(false);
        SceneManager.LoadScene("Assets/Scenes/LevelSelect.unity");
    }
    #endregion
}
