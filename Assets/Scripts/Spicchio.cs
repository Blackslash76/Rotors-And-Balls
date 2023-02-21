using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spicchio : MonoBehaviour
{

    public string PallaAmmessa;


    // Start is called before the first frame update
    void Start()
    {
            Spawner spawnScript = SpawnerData.GetCurrentSpawner().GetComponent<Spawner>();

            //Genero un numero casuale
            int num = Random.Range(0, spawnScript.NumeroDiPalline);

            PallaAmmessa = SpawnerData.ReturnStandardBallByIndex(num).transform.tag;

            //Ora cambio il colore secondo il colore memorizzato nella pallina
            transform.GetComponent<SpriteRenderer>().color = SpawnerData.ReturnStandardBallByIndex(num).GetComponent<BallManager>().coloreBall;

            //Pure del loculo
            transform.parent.GetComponent<SpriteRenderer>().color = transform.GetComponent<SpriteRenderer>().color;
    }
}
