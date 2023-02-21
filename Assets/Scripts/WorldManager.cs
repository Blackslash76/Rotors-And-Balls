using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class WorldManager : MonoBehaviour {

    public GameObject BaseButton;
    private GameObject PannelloButtons;

    public GameObject[] Pannelli;


    private int LivelliInseriti = 0;
    private int PanelIndex = 0;
    public int LevelsForPanel = 20;

    private ScenesList list;

    private GameObject ButtonPressed;

    private Animator anim;

    private void Awake()
    {
        
        //anim GUI
        anim = transform.parent.GetComponent<Animator>();

        list = (ScenesList)Resources.Load("ScenesList");

        //Music
        Main.Audio.PlaySound(Main.Audio.Suoni.LevelSelection);
        BuildGUI();

        //Test
        //Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Question,"Level Select");
    }

    void BuildGUI()
    {
        int startlevel = 1;
        int endlevel = 61;

        #if UNITY_EDITOR
            startlevel = 0;
            endlevel = 71;
        #endif

        if (Debug.isDebugBuild)
        {
            startlevel = 0;
            endlevel = 71;
        }

        LivelliInseriti = 0;
        for (int i = startlevel; i < endlevel; i++)
        {
            LivelliInseriti++;
            if (LivelliInseriti > LevelsForPanel)
            {
                PanelIndex ++;
                LivelliInseriti = 1;
            }
            PannelloButtons = Pannelli[PanelIndex].transform.Find("ListaLivelli").gameObject;
            if (list.scenesPath[i] != "")
            {
                GameObject button = (GameObject)Instantiate(BaseButton);

                button.transform.SetParent(PannelloButtons.transform);
                button.transform.GetComponent<LevelButtonManager>().NumeroLivello = i;
                button.transform.GetComponent<LevelButtonManager>().LevelNumber.GetComponent<Text>().text = "Level " + i;
                button.transform.GetComponent<LevelButtonManager>().LevelDescription.GetComponent<Text>().text = list.scenesDescription[i];
                button.transform.GetComponent<LevelButtonManager>().LevelPath = list.scenesPath[i];
                button.transform.GetComponent<LevelButtonManager>().UpdateMiniatureStars();
                if (Main.Level.LevelsStatusCompleted[i] >= -1)
                {
                    button.transform.GetComponent<LevelButtonManager>().ClosedIcon.SetActive(false);
                    button.GetComponent<Button>().interactable = true;
                }
                else
                {
                    button.transform.GetComponent<LevelButtonManager>().ClosedIcon.SetActive(true);
                    button.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void ButtonClicked(GameObject button)
    {
        LevelButtonManager buttonscript = button.GetComponent<LevelButtonManager>();
        if (!ButtonPressed)
        {
            ButtonPressed = button;
            buttonscript.ToRear_StartAnimation();
            //Sound
            Main.Audio.PlaySoundFX(Main.Audio._audioscript.GUISounds[(int)Main.Audio.SuoniGUI.ButtonClick], 1f);

        }
        else
        {
            if (ButtonPressed == button)
            {
                buttonscript.ToFront_StartAnimation();
                ButtonPressed = null;
                //Sound
                Main.Audio.PlaySoundFX(Main.Audio._audioscript.GUISounds[(int)Main.Audio.SuoniGUI.ButtonClickBack], 1f);
            }
            else
            {
                ButtonPressed.GetComponent<LevelButtonManager>().ToFront_StartAnimation();
                buttonscript.ToRear_StartAnimation();
                ButtonPressed = button;
                //Sound
                Main.Audio.PlaySoundFX(Main.Audio._audioscript.GUISounds[(int)Main.Audio.SuoniGUI.ButtonClick], 1f);
            }
        }
    }

    public void MainMenù_Clicked()
    {
        SceneManager.LoadScene(Main.MainMenuScene);
    }

    public void WorldManager_Transition()
    {
        
        anim.Play("LevelSelect_Transition");
    }
}
