using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetChecker : MonoBehaviour
{
    private static bool created = false;
    private DBManager db;

    private void Awake()
    {
        db = GameObject.FindObjectOfType<DBManager>();
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    private void Start()
    {
        this.StartCoroutine(PingUpdate());
    }


    IEnumerator PingUpdate()
    {
        while (true)
        {
            var ping = new Ping("8.8.8.8");

            yield return new WaitForSeconds(1f);
            while (!ping.isDone)
            {
                Main.ConnessoAInternet = false;
                yield return null;
            }
            
            Main.ConnessoAInternet = true;
            //db.VerificaGUID();
        }
    }
}