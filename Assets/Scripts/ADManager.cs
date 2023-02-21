using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using EnergySuite;


public class ADManager : MonoBehaviour
{

    private static bool created = false;

    private int PremioDaDare;

    public bool ADFinished;
    public bool ADReady;
    private string gameId = "2887792";

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }


    }


    bool testMode = false;

    public string placementId = "rewardedVideo";

    private int amountPrize;


    
    void Start()
    {
#if UNITY_IOS
        gameId = "2887791";
#elif UNITY_ANDROID
        gameId = "2887792";
#elif UNITY_WEBGL
        gameId = "2887792";
#endif


        Advertisement.Initialize(gameId, testMode);
    }

    private void Update()
    {
        ADReady = Advertisement.IsReady(placementId);
    }

    public void ShowAd(int Premio, int amount)
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        if (Advertisement.IsReady(placementId))
        {
            PremioDaDare = Premio;
            amountPrize = amount;
            ADFinished = false;
            Advertisement.Show(placementId, options);
        }
    }


    void HandleShowResult(ShowResult result)
    {
        ADFinished = true;
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            if (PremioDaDare == 0)
            {
                Main.Player.AddSubCoins((sbyte)amountPrize);
            }
            else if (PremioDaDare == 1)
            {
                EnergySuiteManager.Add(TimeValue.Energy, amountPrize);
            }
            else if (PremioDaDare == 2)
            {
                Main.Level.AddExtraTime(amountPrize);
            }

        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}
