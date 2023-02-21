using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins_GUI : MonoBehaviour {

    private TextMeshProUGUI coins_textbox;
    private Animator anim;


    private void Awake()
    {
        coins_textbox = transform.GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
        coins_textbox.SetText("x" + Main.Player.Coins.ToString());
        
	}

    public void PlayAnimation()
    {
        anim.Play("CoinsGui_Update");
    }
}
