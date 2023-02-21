using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
    public string Nome="";
    public string Punteggio="";
    public int Icona;
    public string Guid;
    public string PunteggioFormattato
    {
        get
        {
            return Punteggio;
        }
    }
}
