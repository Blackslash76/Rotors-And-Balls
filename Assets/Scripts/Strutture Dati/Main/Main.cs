using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EnergySuite;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public static class Main
{
    public static string MainMenuScene;
    public static string UID = "";
    public static int StarsCollected
    {
        get
        {
            int sum = 0;
            for (int i = 0; i < Level.LevelsStatusCompleted.Length; i++)
            {
                if (Level.LevelsStatusCompleted[i] >= 0)
                {
                    sum += Level.LevelsStatusCompleted[i] + 1;
                }
            }
            return sum;
        }
    }

    public static class Level
    {
        public static int LevelNumber;
        public static int LevelDifficulty;
        public static int LevelMoneyMultiplier = 1;
        
        public static int[] LevelsStatusCompleted = new int[100];
        public static int[,] Scores = new int[100, 3];
        public static ScoreData InternetScores = new ScoreData();


        public static void InitLevel(int _levelnumber, float _levelTime, float _speedCircuito, float _speedPerimetro)
        {
            GUI.InitGUI();
            InitRotors();

            
            InitLevelTimer(_levelTime);
            InitTubes();


            LevelNumber = _levelnumber;
            LevelSpeedCircuito = _speedCircuito;
            LevelSpeedPerimetro = _speedPerimetro;
            LevelSeriesCompleted = 0;
            LevelMoneyMultiplier = 1;
            InternetScores.Guid = "";

            //Inizializzo lo score
            Main.Player.InitScore();

            //Carico il record score di Internet
            Main.Level.GetInternetScore();
        }

        public static float LevelSpeedPerimetro;
        public static float LevelSpeedCircuito;

        private static float levelStartTime;
        private static float levelCurrentTime;
        private static float levelExtraTime;
        public static float TimeLeft
        {
            get { return Mathf.Round(levelCurrentTime - (Time.time - levelStartTime) + levelExtraTime); }
        }

        public static float LevelCurrentTime
        {
            get { return levelCurrentTime; }
        }

        public static void InitLevelTimer(float _timerlength)
        {
            levelCurrentTime = _timerlength;
            levelStartTime = Time.time;
            levelExtraTime = 0;
        }

        public static void AddExtraTime(float time)
        {
            levelExtraTime += time;
        }

        public static int LevelSeriesCompleted;

        public static CameraManager MainCamera
        {
            get { return Camera.main.GetComponent<CameraManager>(); }
        }

        private static void InitTubes()
        {
            GameObject[] bocchette = GameObject.FindGameObjectsWithTag("Tubo,Verticale");

            foreach (GameObject bocchetta in bocchette)
            {
                if (bocchetta.transform.parent.transform.localEulerAngles.z == 90 || bocchetta.transform.parent.transform.localEulerAngles.z == 270)
                {
                    bocchetta.tag = "Tubo,Orizzontale";
                }
            }

        }
        //ROTORI //
        public static GameObject[] RotoriLivello;


        public static int NumeroDiRotoriNelLivello
        { get { return RotoriLivello.Length; } }
        private static void InitRotors()
        {
            RotoriLivello = GameObject.FindGameObjectsWithTag("Caricatore");
            //Metto l'ombra e i led
            foreach (GameObject shadow in GameObject.FindGameObjectsWithTag("shadow"))
            {
                shadow.transform.localPosition = new Vector2(2, -2);
            }
        }

        // SCORES //
        public static void UpdateLevelScore(int level, int difficulty, int value)
        {
            int InternetScore;
            Int32.TryParse(Main.Level.InternetScores.Punteggio, out InternetScore);

            Scores[level, difficulty] = Mathf.Max(Scores[level, difficulty], value);

            if (Scores[level, difficulty] > InternetScore)
            {
                GameObject.FindObjectOfType<DBManager>().InserisciPunteggio(Main.UID, level, difficulty, value);
            }
        }

        public static void GetInternetScore()
        {
            GameObject.FindObjectOfType<DBManager>().OttieniPunteggio(LevelNumber, LevelDifficulty);
        }

        public static void PostStats(int levelnumber, int difficulty, int timeleft)
        {
            GameObject.FindObjectOfType<DBManager>().InserisciStat(levelnumber, difficulty, timeleft);
        }

    }
    //************************************************************************
    // ******************** PLAYER *******************************************
    //************************************************************************
    public static class Player
    {
        private static List<sbyte> coins_transactions = new List<sbyte>();
        private static List<int> score_transactions = new List<int>();

        public static string PlayerName = "";
        public static int PlayerIcon = 0;
        public static int Score;
        public static int TotalSeriesCompleted;
        public static int Coins;
        public static bool Music = true;
        public static bool SoundFX = true;



        public static void AddScore(int ScoreToAdd)
        {
            score_transactions.Add(ScoreToAdd);
            Score = sumscore();
        }

        public static void InitScore()
        {
            score_transactions = new List<int>();
            Score = 0;
        }

        public static List<int> GetScore()
        {
            return score_transactions;
        }

        public static void LoadScore(List<int> _score)
        {
            score_transactions = _score;
            Score = sumscore();
        }

        private static int sumscore()
        {
            int sum = 0;
            foreach (int i in score_transactions)
            {
                sum += i;
            }
            return sum;
        }

        public static void AddSubCoins(sbyte Value)
        {
            coins_transactions.Add(Value);
            Coins = sumcoins();
        }

        public static void InitCoins()
        {
            coins_transactions = new List<sbyte>();
            Coins = 0;
        }

        public static List<sbyte> GetCoins()
        {
            return coins_transactions;
        }

        public static void LoadCoins(List<sbyte> _coins)
        {
            coins_transactions = _coins;
            Coins = sumcoins();
        }

        private static int sumcoins()
        {
            int sum = 0;
            foreach (sbyte b in coins_transactions)
            {
                sum += b;
            }

            Debug.Log("Somma Monete: " + sum);
            return sum;
        }

    }

    //************************************************************************
    // ******************** GUI **********************************************
    //************************************************************************
    public static class GUI
    {
        private static GuiHub _guihub;
        private static PopupManager _popupmanager;
        private static InfoPanel_Manager _infopanel;

        public enum IconePopup { Ok, Question, Star, Coin, Crown };

        public static void DoCinematicOnObject(GameObject obj, string DescrizioneFunzione)
        {
            Level.MainCamera.Zooma(15, 2f, obj.transform.position.x, obj.transform.position.y);
            _guihub.Cinematic.SetActive(true);
            SetCinematicTxt(DescrizioneFunzione);
            _guihub.Cinematic.GetComponent<NewBallPresentation_GUI>().Ball = obj;
        }

        public static void SetCinematicTxt(string testo)
        {
            _guihub.Cinematic.GetComponent<NewBallPresentation_GUI>().InitText(testo);
        }

        public static void ShowPauseMenu(bool boolean)
        {
            _guihub.Pause.SetActive(boolean);
        }

        public static void ShowGameOverUI(bool boolean)
        {
            _guihub.GameOver.SetActive(boolean);
        }

        public static void ShowEndOfLevelUI(bool boolean)
        {
            _guihub.EndLevel.SetActive(boolean);
        }

        public static void ShowStartLevelUI(bool boolean)
        {

            _guihub.NewLevel.SetActive(boolean);
        }

        public static void ShowNeedMoreEnergyPopup()
        {
            GamePaused = true;
            _popupmanager.NeedMoreTime.SetActive(true);
        }

        public static void InitGUI()
        {
            _guihub = GameObject.FindObjectOfType<GuiHub>();
            _popupmanager = GameObject.FindObjectOfType<PopupManager>();
            _infopanel = GameObject.FindObjectOfType<InfoPanel_Manager>();
        }



        public static void AndroidDebug(object txt)
        {
            _guihub.SetDebugText(txt.ToString(), 10);
        }

        public static void ShowInfoPopup(Main.GUI.IconePopup icona, string testo)
        {
            if (!_infopanel)
            {
                _infopanel = GameObject.FindObjectOfType<InfoPanel_Manager>();
            }
            _infopanel.ShowPopup((int)icona, testo);
        }
    }

    public static class Tutorial
    {
        private static List<string> newObjectNotifyList;

        public static bool CandidateToShow(string tagOggetto)
        {
            if (newObjectNotifyList.Contains(tagOggetto))
            {
                return false;
            }
            else
            {
                newObjectNotifyList.Add(tagOggetto);
                return true;
            }
        }

        public static string CinematicTxt;

        public static List<string> NotifyList
        {
            get { return newObjectNotifyList; }

        }

        public static void SetNotifyList(List<string> lista)
        {
            newObjectNotifyList = lista;
        }

        public static bool itsTutorial = false;
    }

    public static class Audio
    {
        private static AudioSource _audiosource
        {
            get
            {
                if (GameObject.Find("AudioManager"))
                {
                    return GameObject.Find("AudioManager").GetComponent<AudioSource>();
                }
                else
                {
                    return null;
                }
            }
        }
        public static AudioManager _audioscript
        {
            get
            {
                if (GameObject.Find("AudioManager"))
                {
                    return GameObject.Find("AudioManager").GetComponent<AudioManager>();
                }
                else
                {
                    return null;
                }
            }
        }

        public enum Suoni
        {
            InnestoPalla,
            RotazioneRotore,
            RotoreCompleto,
            Divieto,
            LoculoPieno,
            MusicaGioco,
            StartNewLevel,
            MainMenù,
            EndLevel,
            LevelSelection
        }

        public enum SuoniGUI
        {
            ButtonClick,
            ButtonClickBack
        }

        public static void StopAllSounds()
        {
            _audiosource.Stop();
        }

        public static void FadeOutSoundAndStop(float fadetime)
        {
            _audioscript.AudioFadeOut(fadetime);
        }

        #region Elenco Suoni

        public static void PlaySoundFX(AudioClip clip, float volume)
        {
            if (_audiosource)
            {
                _audiosource.PlayOneShot(clip, volume);
            }
        }

        public static void PlaySound(Suoni soundindex)
        {
            if (_audiosource)
            {
                switch (soundindex)
                {
                    case Suoni.InnestoPalla:
                        {

                            PlaySoundEffect(_audioscript.Innesto);
                            break;
                        }
                    case Suoni.RotazioneRotore:
                        {
                            PlaySoundEffect(_audioscript.RotorMovement);
                            break;
                        }
                    case Suoni.RotoreCompleto:
                        {
                            PlaySoundEffect(_audioscript.RotoreCompletato);
                            break;
                        }
                    case Suoni.Divieto:
                        {
                            PlaySoundEffect(_audioscript.TuboSemaforo);
                            break;
                        }
                    case Suoni.LoculoPieno:
                        {
                            PlaySoundEffect(_audioscript.LoculoPieno);
                            break;
                        }
                    case Suoni.MusicaGioco:
                        {
                            int rndmusic = UnityEngine.Random.Range(0, _audioscript.MusicheSottofondo.Count);
                            PlayMusic(_audioscript.MusicheSottofondo[rndmusic]);
                            break;
                        }
                    case Suoni.StartNewLevel:
                        {
                            PlaySoundEffect(_audioscript.StartNewLevel);
                            break;
                        }
                    case Suoni.MainMenù:
                        {
                            PlayMusic(_audioscript.MusicaMenu);
                            break;
                        }
                    case Suoni.LevelSelection:
                        {
                            PlayMusic(_audioscript.LevelSelect);
                            break;
                        }
                    case Suoni.EndLevel:
                        {
                            PlayMusic(_audioscript.EndLevel);
                            break;
                        }
                }
            }
        }
        #endregion
        private static void PlayMusic(AudioClip audio)
        {
            if (Player.Music)
            {
                _audiosource.Stop();
                _audiosource.loop = true;
                _audiosource.clip = audio;
                _audiosource.Play();

            }
        }

        private static void PlaySoundEffect(AudioClip audio)
        {
            if (Player.SoundFX)
            {
                _audiosource.PlayOneShot(audio);
            }
        }

    }

    public static class MoneyMoney
    {
        public static void MostraPubblicitàCoin(int amount)
        {
            ADManager aD = GameObject.FindGameObjectWithTag("Pubblicità").GetComponent<ADManager>();
            aD.ShowAd(0, amount);
        }

        public static void MostraPubblicitàEnergy(int amount)
        {
            ADManager aD = GameObject.FindGameObjectWithTag("Pubblicità").GetComponent<ADManager>();
            aD.ShowAd(1, amount);
        }

        public static void MostraPubblicitàTime(int amount)
        {
            ADManager aD = GameObject.FindGameObjectWithTag("Pubblicità").GetComponent<ADManager>();
            aD.ShowAd(2, amount);
        }
        public static bool ADFinished
        {
            get
            {
                ADManager aD = GameObject.FindGameObjectWithTag("Pubblicità").GetComponent<ADManager>();
                return aD.ADFinished;
            }
        }
        public static bool ADReady
        {
            get
            {
                ADManager aD = GameObject.FindGameObjectWithTag("Pubblicità").GetComponent<ADManager>();
                return aD.ADReady;
            }
        }
}

public static void IncreaseSeriesComplete()
    {
        Level.LevelSeriesCompleted += 1;
        Player.TotalSeriesCompleted += 1;
    }

    //Variabili per gestire complessivamente il gioco
    public static bool GamePaused = false;
    
    public static bool InputStopped = false;

    public static bool ConnessoAInternet = false;

    public static void PauseGame(bool pause)
    {
        //        if (pause != GamePaused)
        if (true)
        {
            PauseManager pausemanager = GameObject.FindObjectOfType<PauseManager>();
            if (pause)
            {
                pausemanager.Init();
            }
            else
            {
                pausemanager.Stop();
            }

            GamePaused = pause;

            Debug.Log("PAUSE---> " + GamePaused);
        }
    }


    public static void InitEverything(bool force)
    {

        int returncode = SaveLoad.Load();

        if (returncode == -1 || force)
        {
            Tutorial.SetNotifyList(new List<string>());
            Player.InitScore();
            Player.InitCoins();
            EnergySuiteManager.Add(TimeValue.Energy, EnergySuiteManager.GetMaxAmount(TimeValue.Energy));


            
            for (int i = 0; i < Level.LevelsStatusCompleted.Length; i++)
            {
                Level.LevelsStatusCompleted[i] = -2;
            }
            Level.LevelsStatusCompleted[1] = -1;

            #if UNITY_EDITOR
            for (int i = 0; i < Level.LevelsStatusCompleted.Length; i++)
            {
                if (Level.LevelsStatusCompleted[i] == -2)
                {
                    Level.LevelsStatusCompleted[i] = -1;
                }
            }
            Level.LevelsStatusCompleted[0] = -1;
            Level.AddExtraTime(20);
            #endif

            if (Debug.isDebugBuild)
            {
                for (int i = 0; i < Level.LevelsStatusCompleted.Length; i++)
                {
                    if (Level.LevelsStatusCompleted[i] == -2)
                    {
                        Level.LevelsStatusCompleted[i] = -1;
                    }
                    }

                Level.AddExtraTime(20);
            }
        }

        Debug.Log("ACTUAL GUID: " + Main.UID);

        GameObject.FindObjectOfType<DBManager>().VerificaGUID();
        SaveLoad.Save();
    }
}

