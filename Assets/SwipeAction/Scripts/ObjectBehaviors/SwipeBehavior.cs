using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: This controls the behavior of the
    melee swipe object when in melee mode
*/

public class SwipeBehavior : MonoBehaviour
{
    bool begin_removal = false;
    float alpha_mod = 1.0f;
    float alive_time = 0.0f;
    Vector3 init_scale = Vector3.zero;

    void Start()
    {
        init_scale = transform.localScale;
        transform.localScale = new Vector3(1.8f * transform.localScale.x, transform.localScale.y, 1.8f * transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!begin_removal && alive_time > 1)
            begin_removal = true;
        else if (begin_removal && alpha_mod > 0)
        {
            GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, alpha_mod);
            alpha_mod -= 2*Time.deltaTime;
        }
        else if (alive_time <= 1)
            alive_time += 2*Time.deltaTime;

        if (init_scale.magnitude < transform.localScale.magnitude)
            transform.localScale = new Vector3(transform.localScale.x - 5*Time.deltaTime, transform.localScale.y, transform.localScale.z - 5*Time.deltaTime);
    }

    public float GetAlphaMod()
    {
        return alpha_mod;
    }

    public void setTexture(Texture2D newSwipe)
    {
        if (transform.GetComponent<Renderer>().material.mainTexture != newSwipe)
            transform.GetComponent<Renderer>().material.mainTexture = newSwipe;
    }
}
