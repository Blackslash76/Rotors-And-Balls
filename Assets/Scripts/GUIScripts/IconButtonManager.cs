using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconButtonManager : MonoBehaviour
{
    public int IconID;

    public void IconButton_Onclick()
    {
        Main.Player.PlayerIcon = IconID;
        transform.parent.gameObject.SetActive(false);
    }
}
