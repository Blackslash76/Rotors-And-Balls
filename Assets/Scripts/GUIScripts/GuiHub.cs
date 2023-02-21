using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuiHub : MonoBehaviour {

    public GameObject Player;
    public GameObject GameOver;
    public GameObject EndLevel;
    public GameObject Cinematic;
    public GameObject Pause;
    public GameObject NewLevel;

    public TextMeshProUGUI DebugText;

    private float debugTxtTimer = 0;

    public void SetDebugText (string txt, float timetoshow)
    {
        debugTxtTimer = timetoshow;
        DebugText.text = txt;

        StartCoroutine(timeoutCheck());
    }


    IEnumerator timeoutCheck()
    {
        float pauseEndTime = Time.realtimeSinceStartup + debugTxtTimer;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        DebugText.text = "";
    }
}
