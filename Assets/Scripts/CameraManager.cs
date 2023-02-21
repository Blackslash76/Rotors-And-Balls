using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour {
    private float zoomEnd;
    private float speedZoom;

    private float posx;
    private float posy;

    private Vector3 origPosition;
    private float origOrtoSize;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        

        origPosition = transform.position;
        origOrtoSize = cam.orthographicSize;
        zoomEnd = origOrtoSize;
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Mathf.Abs(cam.orthographicSize - zoomEnd) >0 )
        {
           cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomEnd, Time.deltaTime * speedZoom);
        }
		
	}

    public void Zooma(float _zoomEnd, float _speedZoom, float _posx, float _posy)
    {
        zoomEnd = _zoomEnd;
        speedZoom = _speedZoom;
        posx = _posx;
        posy = _posy;

        transform.position = new Vector3(posx, posy, transform.position.z);
    }

    public void ResetZoom()
    {
        //Zooma(origOrtoSize, 3, origPosition.x, origPosition.y);
        transform.position = origPosition;
        cam.orthographicSize = origOrtoSize;
        zoomEnd = origOrtoSize;
    }

    public void toPreviewSize()
    {
        cam.rect = new Rect(0.569f, 0.057f, 0.383f, 0.369f);
    }

    public void toNormalSize()
    {
        cam.rect = new Rect(0f, 0f, 1f, 1f);
    }

    public void DisablePreviewCamera()
    {
        transform.Find("Preview Camera").gameObject.SetActive(false);
    }


    public void EnablePreviewCamera()
    {
        transform.Find("Preview Camera").gameObject.SetActive(true);
    }
}
