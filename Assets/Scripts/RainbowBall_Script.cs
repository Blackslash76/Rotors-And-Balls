using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowBall_Script : MonoBehaviour {

    private GameObject spawn;

    private GameObject[] ballsList;

    private void Awake()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawner");
        //spawn.GetComponent<RandomSpawn>().ballsList.CopyTo(ballsList, 0);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
    }

    public void VerificaDopoInnesto()
    {
        int maxValue = 0;
        string maxTag = "";
        int PalleDiverseTrovate = 0;
        int SlotOccupati = 0;

        Rotore RotoreScript = transform.parent.parent.GetComponent<Rotore>();
        foreach(GameObject go in spawn.GetComponent<RandomSpawn>().ballsList)
        {
            string tag = go.transform.tag;
            int val = RotoreScript.BallsInnestateConTag(tag);
            if (val > 0) { PalleDiverseTrovate += 1; }
            if (val>maxValue)
            {
                maxValue = val;
                maxTag = tag;
            }
            SlotOccupati += val;
        }
        //Se c'è una palla dominante e gli slot pieni sono almeno 3 (compresa la Rainball) allora la consumo
        if (PalleDiverseTrovate>1)
        {

            //Cerco la palla che mi interessa
            foreach (GameObject go in spawn.GetComponent<RandomSpawn>().ballsList)
            {
                if (go.transform.tag==maxTag)
                {
                    RotoreScript.TrasformaPallaCasuale(go);

                    break;
                }
            }
        }

    }
}
