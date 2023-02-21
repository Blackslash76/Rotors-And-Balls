using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {
    bool EndOfLevel = false;
    GlobalVariables gb;

    // Use this for initialization
    void Start()
    {
        gb = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
    }

    // Update is called once per frame
    void Update()
    {

        this.EndOfLevelCheck();
    }

    void EndOfLevelCheck()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Caricatore"))
        {
            if (go.GetComponent<Rotore>().HitPoints <= 0)
            {
                EndOfLevel = true;
            }
            else
            {
                EndOfLevel = false;
                break;
            }
        }

        if (EndOfLevel)
        {
            //Finito il livello, carico il prossimo
            gb.SetDebugText("HAI VINTO QUESTA DEMO!");
        }
    }
}
