using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonManager : MonoBehaviour {

    public GameObject LevelNumber;
    public GameObject LevelDescription;
    public GameObject ClosedIcon;
    public GameObject PannelloStelle;

    public GameObject StellinaEasy;
    public GameObject StellinaNormal;
    public GameObject StellinaHard;

    public int NumeroLivello;
    public string LevelPath;
    public bool Activated;

    private Animator anim;
    private WorldManager wm;

	// Use this for initialization
	void Awake ()
    {
        anim = GetComponent<Animator>();
        wm = GameObject.FindObjectOfType<WorldManager>();
	}

    public void OnButton_Click()
    {
        wm.ButtonClicked(this.gameObject);
    }

    private void ToFront_EndRotationAnimation()
    {
        LevelNumber.gameObject.SetActive(true);
        LevelDescription.gameObject.SetActive(true);
    }

    public void ToFront_StartAnimation()
    {
        anim.Play("PulsanteAnimato_ToFront");
        PannelloStelle_Manager pannellostellescript = PannelloStelle.GetComponent<PannelloStelle_Manager>();
        pannellostellescript.Chiudi();
    }

    public void ToRear_StartAnimation()
    {
        PannelloStelle_Manager pannellostellescript = PannelloStelle.GetComponent<PannelloStelle_Manager>();
        anim.Play("PulsanteAnimato_ToRear");
        LevelNumber.gameObject.SetActive(false);
        LevelDescription.gameObject.SetActive(false);
        pannellostellescript.LevelPath = LevelPath;
        pannellostellescript.LevelNumber = NumeroLivello;
        pannellostellescript.SourceButton = this.gameObject;
        PannelloStelle.SetActive(true);
        pannellostellescript.Apri();
    }

    public void UpdateMiniatureStars()
    {
        int lvlcomplete = Main.Level.LevelsStatusCompleted[NumeroLivello];
        if (lvlcomplete >=0)
        {
            StellinaEasy.GetComponent<UnityEngine.UI.Image>().color= new Color(1,1,1);
            if (lvlcomplete >=1)
            {
                StellinaNormal.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1);
                if (lvlcomplete ==2)
                {
                    StellinaHard.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1);
                }
            }
        }
    }
}
