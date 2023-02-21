using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{

    public bool ballCollision = false;
    public int LivelloCorrente = 1;

    public float CurrentLevelTime;

    public float starttime;
    public float TimeElapsed()
    {
        return Time.time - CurrentLevelTime;
    }

    public float TimeLeft()
    {
        return Mathf.Round(CurrentLevelTime - Time.time);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDebugText(string testo)
    {
        GameObject.Find("DebugText").GetComponent<TextMesh>().text = testo;
    }

}
