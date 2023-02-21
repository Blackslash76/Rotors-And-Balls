using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject NeedMoreTime;

    public void NeedMoreTime_YESButton()
    {
        Main.MoneyMoney.MostraPubblicitàTime(60 - (Main.Level.LevelDifficulty*20));
        StartCoroutine(WaitForADS());
    }

    public void NeedMoreTime_NOButton()
    {
        NeedMoreTime_ClosePopup();
    }

    IEnumerator WaitForADS()
    {
        while (!Main.MoneyMoney.ADFinished)
        {
            yield return null;
        }
        NeedMoreTime_ClosePopup();
    }

    public void NeedMoreTime_ClosePopup()
    {
        Main.PauseGame(false);
        NeedMoreTime.SetActive(false);
    }
}
