using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfilePanel : MonoBehaviour
{
    private Animator anim;
    public TMPro.TMP_InputField PlayerName;
    public GameObject ActiveIcon;
    public GameObject IconsPanel;
    public Button OKButton;

    private int activeIcon = -1;
    private IconsList iconslist;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        iconslist = GameObject.FindObjectOfType<IconsList>();
    }

    private void Update()
    {
        if (Main.Player.PlayerIcon != activeIcon)
        {
            ActiveIcon.GetComponent<UnityEngine.UI.Image>().sprite = iconslist.IconsProfile[Main.Player.PlayerIcon];
            activeIcon = Main.Player.PlayerIcon;
        }

        if (PlayerName.text == "" )
        {
            OKButton.interactable = false;
        }
        else
        {
            OKButton.interactable = true;
        }
    }


    void OnEnable()
    {
        anim.Play("GUI_Rect_PopupShow");
    }

    public void OK_Click()
    {
        Main.Player.PlayerName = PlayerName.text;
        SaveLoad.Save();
        GameObject.FindObjectOfType<DBManager>().AggiornaGUID();

        anim.Play("GUI_Rect_PopupHide");
        StartCoroutine(Hide(0.5f));
    }

    public void IconSelect_Clicked()
    {
        IconsPanel.SetActive(true);
    }

    IEnumerator Hide(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.SetActive(false);
    }
}
