using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StellaButton_Manager : MonoBehaviour
{
    public enum LivelliDifficoltà { EASY, NORMAL, HARD }
    public LivelliDifficoltà livellodifficoltà = LivelliDifficoltà.EASY;


    public GameObject PannelloStelle;
    public GameObject OK_Icon;
    public AnimationClip[] AnimazioniAttesa;
    public AnimationClip[] AnimazioniVittoria;

    private PannelloStelle_Manager pannello;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pannello = PannelloStelle.GetComponent<PannelloStelle_Manager>();

    }
    // Start is called before the first frame update
    void OnEnable()
    {
        if (Main.Level.LevelsStatusCompleted[pannello.LevelNumber] >= (int)livellodifficoltà)
        {
            int rnd = Random.Range(0, AnimazioniVittoria.Length);
            float rndspeed = (float)System.Math.Round(Random.Range(0.24f, 0.26f), 2);
            
            //anim.speed = Random.Range(23, 26) / 100;

            anim.Play(AnimazioniVittoria[rnd].name);

            transform.GetComponent<Image>().color = new Color(1, 1, 1);
            OK_Icon.SetActive(true);
        }
        else
        {
            if (Main.Level.LevelsStatusCompleted[pannello.LevelNumber] == (int)livellodifficoltà - 1)
            {
                int rnd = Random.Range(0, AnimazioniAttesa.Length);
                //anim.speed = Random.Range(23, 26) / 100;
                anim.Play(AnimazioniAttesa[rnd].name);
                OK_Icon.SetActive(false);
            }
            if (Main.Level.LevelsStatusCompleted[pannello.LevelNumber] < (int)livellodifficoltà - 1)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void StellaButton_OnClick()
    {
        //WorldManager wm = GameObject.FindObjectOfType<WorldManager>();
        Main.Level.LevelDifficulty = (int)livellodifficoltà;
        
        SceneManager.LoadScene(pannello.LevelPath);
        //wm.WorldManager_Transition();
    }
}
