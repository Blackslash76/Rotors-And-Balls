using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour {

    public GameObject scoreText;
    public int scoreLength = 7;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateScore();
	}

    public void UpdateScore()
    {

        //Ottengo la scritta dello score secondo il numero di caratteri desiderato
        string testo = Main.Player.Score.ToString(new string('0' ,scoreLength));

        
        scoreText.GetComponent<TextMeshProUGUI>().text = testo;
    }
}
