using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLightManager : MonoBehaviour {
    private Quaternion originalRotation;
    private Vector3 offset;


	// Use this for initialization
	void Start () {
        originalRotation = transform.rotation;
        offset = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.rotation != originalRotation)
        {
            Vector3 newposition = new Vector3(transform.parent.position.x + offset.x, transform.parent.position.y + offset.y, transform.parent.position.z+offset.z);

            transform.position = newposition;
            transform.rotation = originalRotation;
            
        }
	}
}
