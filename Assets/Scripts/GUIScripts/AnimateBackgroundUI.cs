using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateBackgroundUI : MonoBehaviour
{
    private RawImage img;
    public float speed;
    private int direzione;
    // Start is called before the first frame update
    void Start()
    {
        img = transform.GetComponent<RawImage>();
        direzione = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        img.uvRect= new Rect (0, img.uvRect.y + (Time.deltaTime * speed * direzione), 1, 1);
        if (direzione == -1 && img.uvRect.y <= -0.5f)
        {
            direzione = 1;
        }
        if (direzione == 1 && img.uvRect.y >= 0f)
        {
            direzione = -1;
        }

    }
}
