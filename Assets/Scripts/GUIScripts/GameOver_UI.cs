using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_UI : MonoBehaviour {

    public GameObject MainMenu_UI;

    // Use this for initialization
    void Start () {
        StartCoroutine(FadeEffect.FadeCanvas(GetComponent<CanvasGroup>(), 0f, 1f, 1f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver_Onclick()
    {

        //MainMenu_UI.SetActive(true);
        //this.gameObject.SetActive(false);

        //Main.Audio.FadeOutSoundAndStop(0.1f);
        Main.Audio.StopAllSounds();

        //Salvo i dati correnti
        SaveLoad.Save();

        Main.PauseGame(false);
        SceneManager.LoadScene(Main.MainMenuScene);
    }
}
