using UnityEngine;

public class AppPaused : MonoBehaviour
{
    bool isPaused = false;

    void OnGUI()
    {
        if (isPaused && Main.MoneyMoney.ADFinished)
            Main.GUI.ShowPauseMenu(true);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }
}