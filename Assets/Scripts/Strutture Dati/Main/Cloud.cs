
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class Cloud
{
    public static bool Autenticato;
    public static string CloudId;
    public static string CloudName;

    public static void AggiornaClassifica(int difficoltà)
    {
        string Leaderboard_ID = "";

        int score = 10;

        //for (int i=0;i<100;i++)
        //{
        //    score += Main.Level.Scores[difficoltà, i];
        //}

        if (difficoltà == 0)
        {
            Leaderboard_ID = GPGSIds.leaderboard_leaderboard_easy;
        }
        else if (difficoltà == 1)
        {
            Leaderboard_ID = GPGSIds.leaderboard_leaderboard_normal;
        }
        else if (difficoltà == 2)
        {
            Leaderboard_ID = GPGSIds.leaderboard_leaderboard_hard;
        }

        if (Autenticato)
        {
            Social.ReportScore(score, Leaderboard_ID, (bool success) =>
            {
                // handle success or failure
                if (success)
                {
                    Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Ok, "OK..."+Leaderboard_ID);
                }
                else
                {
                    Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Question, "Classifica KO");
                }
            });
        }
    }
}