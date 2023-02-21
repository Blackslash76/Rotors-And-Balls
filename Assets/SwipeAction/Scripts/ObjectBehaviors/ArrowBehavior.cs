using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: This controls the behavior of the
    ArrowTemplate object when in range mode
*/

public class ArrowBehavior : MonoBehaviour
{
    bool begin_removal = false;
    float alpha_mod = 1.0f;

    // Update is called once per frame
	void Update()
    {
        if (!begin_removal && transform.GetComponent<Rigidbody>().velocity.magnitude <= 0.001f)
            begin_removal = true;
        else if (begin_removal && alpha_mod > 0)
        {
            GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, alpha_mod);
            alpha_mod -= Time.deltaTime;
        }
	}

    void OnTriggerEnter(Collider other_obj)
    {
        if (other_obj.transform.name.Contains("housewall"))
        {
            GetComponent<Rigidbody>().velocity = -GetComponent<Rigidbody>().mass * GetComponent<Rigidbody>().velocity / 50;
        }
    }

    public void SetProjectileForceDirection(Vector3 force_power, Vector3 force_dir)
    {
        if (force_power.magnitude > 500)
            force_power = new Vector3(500, 0, 50);

        transform.GetComponent<Rigidbody>().velocity = new Vector3(0.1f, 0.1f, 0.1f);
        transform.GetComponent<Rigidbody>().AddForceAtPosition(force_power.magnitude * force_dir, transform.position, ForceMode.Force);
    }

    public float GetAlphaMod()
    {
        return alpha_mod;
    }
}
