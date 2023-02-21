using UnityEngine;
using System.Collections;

public class VolaVersoContatore : MonoBehaviour
{
    private GameObject meter;
    private void Awake()
    {
        meter = GameObject.FindGameObjectWithTag("Level");
    }

    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, meter.transform.position, 3.0f * Time.deltaTime);
        
        transform.localScale = new Vector3(transform.localScale.x - (1.0f * Time.deltaTime), transform.localScale.y - (1.0f * Time.deltaTime));
        if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

}
