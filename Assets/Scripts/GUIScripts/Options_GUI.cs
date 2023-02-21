using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options_GUI : MonoBehaviour
{
    public TMP_InputField PlayerName;
    public Toggle MusicToggle;
    public Toggle SoundFXToggle;
    public GameObject ActiveIcon;

    public GameObject IconsPanel;

    private IconsList iconslist;

    private int activeIcon=-1;
    // Start is called before the first frame update
    void Start()
    {
        MusicToggle.isOn = Main.Player.Music;
        SoundFXToggle.isOn = Main.Player.SoundFX;
        PlayerName.text = Main.Player.PlayerName;

        iconslist = GameObject.FindObjectOfType<IconsList>();
        
    }

    private void Update()
    {
        if (Main.Player.PlayerIcon != activeIcon)
        {
            ActiveIcon.GetComponent<Image>().sprite = iconslist.IconsProfile[Main.Player.PlayerIcon];
            activeIcon = Main.Player.PlayerIcon;
        }
    }

    public void MusicToggle_Changed()
    {
        Main.Player.Music = MusicToggle.isOn;
    }

    public void SoundFXToggle_Changed()
    {
        Main.Player.SoundFX = SoundFXToggle.isOn;
    }

    public void MainMenuButton_Clicked()
    {
        Main.Player.PlayerName = PlayerName.text;
        if (Main.Player.Music==false)
        {
            Main.Audio.StopAllSounds();
        }

        SaveLoad.Save();
        GameObject.FindObjectOfType<DBManager>().AggiornaGUID();

        this.gameObject.SetActive(false);
    }

    public void IconSelect_Clicked()
    {
        IconsPanel.SetActive(true);
    }

}
