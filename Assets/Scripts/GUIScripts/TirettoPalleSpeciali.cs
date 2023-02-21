using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirettoPalleSpeciali : MonoBehaviour {

    private bool isClosing = false;
    private bool isOpening = false;
    private bool Closed = true;

    private float speed = 300;

    private Vector2 origPos;
    private Vector2 maxPos;
    private float spostamentoX = 140;


    private void Awake()
    {
        origPos = transform.position;
        maxPos = new Vector2(transform.position.x - spostamentoX, origPos.y);
    }

    void Update () {
		if (isClosing)
        {
            Vector2 movement = new Vector2(transform.position.x + (speed * Time.fixedDeltaTime), origPos.y);

            transform.position = movement;
            if (movement.x > origPos.x)
            {
                transform.position = origPos;
                isClosing = false;
                Closed = true;
            }
            
        }
        if (isOpening)
        {
            Vector2 movement = new Vector2(transform.position.x - (speed * Time.fixedDeltaTime), origPos.y);

            transform.position = movement;
            if (movement.x < maxPos.x)
            {
                transform.position = maxPos;
                isOpening = false;
                Closed = false;
            }
        }
    }

    public void OpenCloseTiretto_OnClick()
    {
        Debug.Log("HAI CLICCATOOOOOO");
        if (!isClosing && !isOpening)
        {
            if (Closed) {
                isOpening = true;
            }
            else {
                Debug.Log("MI CHIUDOOOOOO");
                isClosing = true;
            }
        }
    }


}
