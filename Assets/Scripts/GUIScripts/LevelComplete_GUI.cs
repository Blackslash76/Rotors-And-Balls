using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete_GUI : MonoBehaviour {

    public GameObject timeLeftText;
    public GameObject seriesText;
    public GameObject pointsText;
    public GameObject scoreText;

    private int multiplierForSeriesCompleted = 500;
    private int multiplierForSecondsLeft = 100;
    private int multiplierForDifficultyLevel = 1;
    

    // Use this for initialization
    void OnEnable()
    {
        //Calcolo dei punti
        int points;

        if (!Main.Tutorial.itsTutorial)
        {
            points =
                Mathf.RoundToInt(
                    (Main.Level.TimeLeft * multiplierForSecondsLeft +
                    Main.Level.LevelSeriesCompleted * multiplierForSeriesCompleted) *
                    multiplierForDifficultyLevel
                );

            Main.Player.AddScore(points);
            //Stop Sound
            //Main.Audio.FadeOutSoundAndStop(0.1f);
            Main.Audio.PlaySound(Main.Audio.Suoni.EndLevel);
        }
        else
        {
            points = 0;
        }
        
        Main.Player.AddScore(points);

        timeLeftText.GetComponent<TextMeshProUGUI>().text = Main.Level.TimeLeft.ToString() + " Sec.";
        seriesText.GetComponent<TextMeshProUGUI>().text = Main.Level.LevelSeriesCompleted.ToString();
        pointsText.GetComponent<TextMeshProUGUI>().text = points.ToString();
        scoreText.GetComponent<TextMeshProUGUI>().text = Main.Player.Score.ToString(new string('0', 7));
    }
    
    public void OnFinishButton_Click()
    {

        //FINITO LIVELLO
        if (!Main.Tutorial.itsTutorial)
        {
            if (Main.Level.LevelsStatusCompleted[Main.Level.LevelNumber + 1] <= -2)
            {
                Main.Level.LevelsStatusCompleted[Main.Level.LevelNumber + 1] = -1;
            }

            if (Main.Level.LevelsStatusCompleted[Main.Level.LevelNumber] > -1)
            {
                Main.Level.UpdateLevelScore(Main.Level.LevelNumber, Main.Level.LevelDifficulty, Main.Player.Score);
                Main.Level.PostStats(Main.Level.LevelNumber, Main.Level.LevelDifficulty, Mathf.RoundToInt(Main.Level.TimeLeft));
            }

            Main.Level.LevelsStatusCompleted[Main.Level.LevelNumber] = Main.Level.LevelDifficulty;
            //Salvo i dati correnti
            SaveLoad.Save();

            //Main.PauseGame(false);
            SceneManager.LoadScene("Assets/Scenes/LevelSelect.unity");
        }
        else
        {
            Main.Tutorial.itsTutorial = false;
            SceneManager.LoadScene(Main.MainMenuScene);
        }
    }
}
