using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScintillaBallsManager : MonoBehaviour {
    private bool IsAnimating = false;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        IsAnimating = true;
        Main.Audio.PlaySound(Main.Audio.Suoni.LoculoPieno);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (IsAnimating)
        {
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
