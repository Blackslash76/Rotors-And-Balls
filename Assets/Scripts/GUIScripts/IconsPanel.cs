using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsPanel : MonoBehaviour
{
    public GameObject IconButton;

    private IconsList iconslist;

    // Start is called before the first frame update
    void Start()
    {
        iconslist = GameObject.FindObjectOfType<IconsList>();

        int i = 0;
        foreach (Sprite sprite in iconslist.IconsProfile)
        {
            GameObject button = (GameObject)Instantiate(IconButton);

            button.transform.SetParent(this.transform);
            button.GetComponent<IconButtonManager>().IconID = i;
            button.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
            i++;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
