using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LucidoPannello : MonoBehaviour
{
    public Material Materiale;
    private Material mat;
    private float lucido_loc;

    public float randomMin = 5f;
    public float randomMax = 20f;
    
    // Start is called before the first frame update

    void Start()
    {
        transform.GetComponent<Image>().material = new Material(Materiale);
        mat = transform.GetComponent<Image>().material;

        StartCoroutine(WaitAndLucido());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitAndLucido()
    {
        yield return new WaitForSeconds(Random.Range(randomMin, randomMax));
        StartCoroutine(FaiLucido());       
    }

    IEnumerator FaiLucido()
    {
        lucido_loc = 0;
        while (lucido_loc < 1)
        {
            lucido_loc += 0.2f;
            mat.SetFloat("_ShineLocation", lucido_loc);
            yield return new WaitForSeconds(0.01f);
        }

        Start();
    }
}
