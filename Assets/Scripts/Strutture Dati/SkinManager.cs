using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkinManager
{
    private static Dictionary<string, GameObject> Oggetti = new Dictionary<string, GameObject>();

    public static void Init()
    {
        //AL MOMENTO INIT INIZIALIZZA GLI OGGETTI SULLO STANDARD, IN FUTURO CONTO DI IMPLEMENTARE
        // UN SISTEMA CON I GUID MA VISTO CHE UNITY NON SUPPORTA IL GUID LO GENERO PRIMA E LO SALVO
        //COME STRINGA.
        if (Oggetti.Count == 0)
        {
            Oggetti.Add("BallRed", (GameObject)Resources.Load("(Skin)Default/Balls/ballRed"));
            Oggetti.Add("BallBlue", (GameObject)Resources.Load("(Skin)Default/Balls/ballBlue"));
            Oggetti.Add("BallGreen", (GameObject)Resources.Load("(Skin)Default/Balls/ballGreen"));
            Oggetti.Add("BallYellow", (GameObject)Resources.Load("(Skin)Default/Balls/ballYellow"));
        }
        }

    public static GameObject GetObject(string nome_oggetto)
    {
        //RESTITUISCE IL GAMEOBJECT ASSEGNATO A QUEL SPECIFICO NOME
        return Oggetti[nome_oggetto];
    }

    private static void GetID()
    {

    }

}
