using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsList : MonoBehaviour
{
    public List<Sprite> IconsProfile = new List<Sprite>();
    public List<Sprite> IconsPopup = new List<Sprite>();

    private static bool created = false;


    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
}
