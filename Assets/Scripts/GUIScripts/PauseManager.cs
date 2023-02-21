using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private float PauseTime;
    private bool Stopped = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Stopped)
        {
            Main.Level.AddExtraTime(Time.time - PauseTime);
            PauseTime = Time.time;
        }
    }

    public void Init()
    {
        Stopped = false;
        PauseTime = Time.time;
    }

    public void Stop()
    {
        Stopped = true;
        PauseTime = 0;
    }
}
