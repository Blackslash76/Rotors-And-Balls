using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject OptionsPanel;
    public GameObject PlayerProfilePanel;

    private void Awake()
    {
        Main.Audio.StopAllSounds();
        Main.MainMenuScene = SceneManager.GetActiveScene().name;
        Main.InitEverything(false);
        Main.Audio.PlaySound(Main.Audio.Suoni.MainMenù);

        if (Main.Player.PlayerName == "" || Main.Player.PlayerName == "Player" || Main.Player.PlayerName=="Debug")
        {
            PlayerProfilePanel.SetActive(true);
        }
        else
        {
            
            string str = "Welcome Back " + Main.Player.PlayerName;
            Debug.Log(str);
            Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Star, str);
        }
    }

    public void PlayButton_Click()
    {
        SkinManager.Init();
        SpawnerData.Init();

        SceneManager.LoadScene("Assets/Scenes/LevelSelect.unity");

    }


    public void TutorialButton_Click()
    {
        SceneManager.LoadScene("Assets/Scenes/Tutorial.unity");
    }

    public void ResetAll_Click()
    {
        Main.InitEverything(true);
    }

    public void OptionsButton_Click()
    {
        OptionsPanel.SetActive(true);
    }

    public void Leaderboard_Click()
    {
        if (Cloud.Autenticato)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void ExitGameButton_Click()
    {
        Application.Quit();
    }
}
