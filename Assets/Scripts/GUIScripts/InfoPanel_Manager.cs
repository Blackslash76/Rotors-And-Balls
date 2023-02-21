using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel_Manager : MonoBehaviour
{
    private static bool created = false;
    private IconsList iconlist;

    public GameObject[] InfoWindows;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        iconlist = GameObject.FindObjectOfType<IconsList>();
    }

    public void ShowPopup(int iconaIndex, string testo)
    {
        bool assigned = false;
        foreach (GameObject window in InfoWindows)
        {
            if (!assigned)
            {
                if (window.GetComponent<CanvasGroup>().alpha == 0)
                {
                    //Scelgo te
                    iconlist = GameObject.FindObjectOfType<IconsList>();
                    window.transform.Find("Testo").GetComponent<Text>().text = testo;
                    window.transform.Find("Icona").GetComponent<Image>().sprite = iconlist.IconsPopup[iconaIndex];
                    StartCoroutine(FadeEffect.FadeCanvas(window.GetComponent<CanvasGroup>(), 0f, 1f, 1f));
                    StartCoroutine(FadeOffDelayed(3f, window.GetComponent<CanvasGroup>()));
                    assigned = true;
                }
            }
        }
    }

    IEnumerator FadeOffDelayed(float time, CanvasGroup window)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(FadeEffect.FadeCanvas(window, 1f, 0f, 1f));
    }


}
