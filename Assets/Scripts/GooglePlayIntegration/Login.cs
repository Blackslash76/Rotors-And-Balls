using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class Login : MonoBehaviour
{
    private System.Action<bool> mAuthCallback;
    private bool mAuthOnStart = true;
    private bool mSigningIn = false;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }


    // Use this for initialization
    void Start()
    {

        mAuthCallback = (bool success) =>
        {

            Debug.Log("In Auth callback, success = " + success);

            mSigningIn = false;
            if (success)
            {
                //START GAME
                Debug.Log("Auth Success!!");
                
                Cloud.Autenticato = true;
                Cloud.CloudId = Social.localUser.id;
                Cloud.CloudName = Social.localUser.userName;

                Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Ok,Cloud.CloudId + " - "+Cloud.CloudName );
                Cloud.AggiornaClassifica(0);
            }
            else
            {
                Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Ok, "Auth Failed!");
            }
        };

        // enable debug logs (note: we do this because this is a sample;
        // on your production app, you probably don't want this turned 
        // on by default, as it will fill the user's logs with debug info).
        var config = new PlayGamesClientConfiguration.Builder()
        .WithInvitationDelegate(InvitationManager.Instance.OnInvitationReceived)
        .EnableSavedGames()
        .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        GooglePlayGames.PlayGamesPlatform.Activate();

        // try silent authentication
        if (mAuthOnStart)
        {
            Authorize(false);
        }

    }

    //Starts the signin process.
    void Authorize(bool silent)
    {
        if (!mSigningIn)
        {
            Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Ok, "Starting sign-in");
            Debug.Log("Starting sign-in...");
            PlayGamesPlatform.Instance.Authenticate(mAuthCallback, silent);
        }
        else
        {
            Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Ok, "Sto già autenticandomi!");

        }
    }

}
