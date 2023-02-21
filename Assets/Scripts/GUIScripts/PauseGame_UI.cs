using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame_UI : MonoBehaviour {

    public GameObject pauseMenu;


    private void Awake()
    {
        //pauseMenu.SetActive(false);
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        Debug.Log("PAUSE INVOCATED");
        Main.PauseGame(true);
    }

    public void ExitPauseButton_OnButton()
    {
        Main.PauseGame(false);
        pauseMenu.SetActive(false);
    }

    public void ExitGameButton_OnButton()
    {
        Application.Quit();
    }

    public void MainMenu_OnButton()
    {
        Main.Audio.StopAllSounds();

        //Salvo i dati correnti
        SaveLoad.Save();

        SceneManager.LoadScene(Main.MainMenuScene);
    }
}

