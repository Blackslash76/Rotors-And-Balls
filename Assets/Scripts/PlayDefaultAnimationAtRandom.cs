using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDefaultAnimationAtRandom : MonoBehaviour
{
    private Animator anim;

    public string Animazione;
    public float randomRangeStart = 15f;
    public float randomRangeEnd = 20f;

    private void Awake()
    {
        anim = transform.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayAnimation()
    {
        while (true)
        {
            float randomWait = Random.Range(randomRangeStart, randomRangeEnd);
            yield return new WaitForSeconds(randomWait);
            //Debug.Log("ANIMAZIONE");
            anim.Play(Animazione);  
        }
    }
}
