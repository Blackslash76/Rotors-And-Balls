using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBallPresentation_GUI : MonoBehaviour {

    private Camera cam;

    public TextMeshProUGUI testo;

    public GameObject Ball;


    private float startTimeout;

    private float timeOut = 5;


    private void Awake()
    {
        cam = Camera.main;
    }

    // Use this for initialization
    void Start () {


    }

    private void OnEnable()
    {
        startTimeout = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update () {
		
        if (Time.realtimeSinceStartup - startTimeout >= timeOut)
        {
            CloseCinematic();
        }
	}

    public void Cinematic_Onclick()
    {
        CloseCinematic();
    }

    public void InitText(string TestoDescrizione)
    {
        testo.text = TestoDescrizione;
    }

    private void CloseCinematic()
    {
        cam.GetComponent<CameraManager>().ResetZoom();
        this.gameObject.SetActive(false);
        Main.PauseGame(false);
        Ball.GetComponent<BallManager>().SetPauseBall(false);
    }

}
