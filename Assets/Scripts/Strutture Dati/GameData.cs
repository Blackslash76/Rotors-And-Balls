using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public static GameData current;
    public List<string> NewBallsNotify;
    public List<sbyte> Coins;
    public int[] LevelsStatusCompleted;
    public int[,] Scores;
    public string GUID;
    public string PlayerName;
    public int PlayerIcon;

    public bool Music;
    public bool SoundFX;
    
}
