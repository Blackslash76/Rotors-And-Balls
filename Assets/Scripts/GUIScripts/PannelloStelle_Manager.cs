using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PannelloStelle_Manager : MonoBehaviour
{
    public GameObject Stella;
    public string LevelPath;
    public int LevelNumber;
    public GameObject SourceButton;
    private Animator anim;
    private RectTransform rt;
    private CanvasGroup canvasgroup;


    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        rt = this.GetComponent<RectTransform>();
        canvasgroup = this.GetComponent<CanvasGroup>();
    }

    public void Apri()
    {

        anim.Play("PannelloStelle_Open");
        MoveDown(this.GetComponent<RectTransform>());
        canvasgroup.interactable = true;
        
    }

    public void Chiudi()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PannelloStelle_Close"))
        {
            MoveUp(this.GetComponent<RectTransform>());
            anim.Play("PannelloStelle_Close");
            canvasgroup.interactable = false;
        }
    }

    private void CloseAnim_Ended()
    {
        this.gameObject.SetActive(false);
    }

    private void MoveDown(RectTransform panel)
    {
        if (rt.position.y > 400)
        {
            StartCoroutine(Move(rt, new Vector2(26, -85)));
        }
        else
        {
            StartCoroutine(Move(rt, new Vector2(26, 170)));
        }
    }

    private void MoveUp(RectTransform panel)
    {
        StartCoroutine(Move(rt, new Vector2(26, 85)));
    }


    IEnumerator Move(RectTransform rt, Vector2 targetPos)
    {
        float step = 0;
        while (step < 1)
        {
            rt.offsetMin = Vector2.Lerp(rt.offsetMin, targetPos, step += Time.deltaTime);
            rt.offsetMax = Vector2.Lerp(rt.offsetMax, targetPos, step += Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }


}
