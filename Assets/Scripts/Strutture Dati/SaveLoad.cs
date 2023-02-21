using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public static class SaveLoad {

    public static GameData savedata = new GameData();
    private static MemoryStream CloudStream = new MemoryStream();

    public static void Save()
    {
        savedata.NewBallsNotify = Main.Tutorial.NotifyList;
        savedata.Coins = Main.Player.GetCoins();
        savedata.Scores = Main.Level.Scores;
        savedata.LevelsStatusCompleted = Main.Level.LevelsStatusCompleted;
        savedata.GUID = Main.UID;
        savedata.PlayerName = Main.Player.PlayerName;
        savedata.PlayerIcon = Main.Player.PlayerIcon;
        savedata.Music = Main.Player.Music;
        savedata.SoundFX = Main.Player.SoundFX;


        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        
        bf.Serialize(file, SaveLoad.savedata);
        bf.Serialize(CloudStream, SaveLoad.savedata);
        file.Close();
        SaveToCloud("Autosave");
    }

    public static int Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedata = (GameData)bf.Deserialize(file);
            file.Close();

            Main.Tutorial.SetNotifyList(savedata.NewBallsNotify);
            Main.Player.LoadCoins(savedata.Coins);
            Main.Level.LevelsStatusCompleted = savedata.LevelsStatusCompleted;
            Main.Level.Scores = savedata.Scores;
            Main.UID = savedata.GUID;
            Main.Player.PlayerName = savedata.PlayerName;
            Main.Player.PlayerIcon = savedata.PlayerIcon;
            Main.Player.Music = savedata.Music;
            Main.Player.SoundFX = savedata.SoundFX;

            return 0;
        }
        else
        { return -1; }
    }

    static void SaveToCloud(string filename)
    {
        if (Cloud.Autenticato)
        {
            // save to named file
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(filename,
                DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                SavedGameOpened);
        }
    }

    public static void SavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
                if (mScreenImage == null)
                {
                    CaptureScreenshot();
                }
                byte[] pngData = (mScreenImage != null) ? mScreenImage.EncodeToPNG() : null;
                Debug.Log("Saving to " + game);
                byte[] data = CloudStream.ToArray();

                TimeSpan playedTime = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
                SavedGameMetadataUpdate.Builder builder = new
                SavedGameMetadataUpdate.Builder()
                    .WithUpdatedPlayedTime(playedTime)
                    .WithUpdatedDescription("Saved Game at " + DateTime.Now);

                if (pngData != null)
                {
                    Debug.Log("Save image of len " + pngData.Length);
                    builder = builder.WithUpdatedPngCoverImage(pngData);
                }
                else
                {
                    Debug.Log("No image avail");
                }
                SavedGameMetadataUpdate updatedMetadata = builder.Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, updatedMetadata, data, SavedGameWritten);
            }
        
        else
        {
            Debug.LogWarning("Error opening game: " + status);
        }
    }

    private static Texture2D mScreenImage;

    public static void CaptureScreenshot()
    {
        mScreenImage = new Texture2D(Screen.width, Screen.height);
        mScreenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        mScreenImage.Apply();
        Debug.Log("Captured screen: " + mScreenImage);
    }

    public static void SavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Ok, "Game " + game.Description + " written");
        }
        else
        {
            Main.GUI.ShowInfoPopup(Main.GUI.IconePopup.Ok, "Error saving game: " + status);
        }
    }
}
