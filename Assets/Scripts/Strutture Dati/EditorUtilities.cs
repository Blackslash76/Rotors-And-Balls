#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using System;

[UnityEditor.InitializeOnLoad]
static class EditorUtilities
{
    [MenuItem("Comodità/Resetta Coordinata Z")]
    static void ResettaCoordinataZ()
    {
        foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (go.transform.localPosition.z < 0 && go.tag!="MainCamera")
            {
                go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0);
            }
        }

    }

    [MenuItem("Comodità/Gioca dalla scena principale")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Main Menu.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Comodità/Apri Officina")]
    public static void ApriOfficinaInEditor()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Officina delle prove.unity");
    }

    [MenuItem("Comodità/Apri Area51 nell'Editor")]
    public static void ApriArea51InEditor()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Level 00 Area 51.unity");
    }


    static EditorUtilities()
    {
        UnityEditor.SceneManagement.EditorSceneManager.sceneSaving += OnSceneSaved;
    }

    static void OnSceneSaved(UnityEngine.SceneManagement.Scene scene, string path)
    {

        // First, try to load the list if already exists
        ScenesList list = (ScenesList)AssetDatabase.LoadAssetAtPath("Assets/Resources/ScenesList.asset", typeof(ScenesList));

        // If doesn't exist, create it !
        if (list == null)
        {
            list = ScriptableObject.CreateInstance<ScenesList>();
            AssetDatabase.CreateAsset(list, "Assets/Resources/ScenesList.asset");
        }

        //Ottengo informazioni sulla scena
        GameObject[] OggettiScena = scene.GetRootGameObjects();
        string DescrizioneLivello="";
        int NumeroLivello=-1;

        foreach (GameObject go in OggettiScena)
        {
            if (go.tag == "Level")
            {
                DescrizioneLivello = go.transform.GetComponent<LevelManager>().levelDescription;
                NumeroLivello = go.transform.GetComponent<LevelManager>().levelNumberInWorld;
            }
        }

        //Se il numero di livello è superiore a -1 allora lo metto in lista (per esempio il tutorial o il main menù avranno sempre valore -1
        // Ora salvo il numero di livello
        if (NumeroLivello > -1)
        {
            list.scenesPath[NumeroLivello] = scene.path;
            list.scenesDescription[NumeroLivello] = DescrizioneLivello;

            EditorUtility.SetDirty(list);
            // Writes all unsaved asset changes to disk
//            AssetDatabase.SaveAssets();
        }
    }

    [PostProcessBuildAttribute(0)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {

        //NOTE: Script expects X.X.X to be your bundleVersion ALREADY. In order for this to work, set your current version number and build number for Android and iOS to your next version (e.g. 1.2.204)

        string currentVersion = PlayerSettings.bundleVersion;

        try
        {
            int major = Convert.ToInt32(currentVersion.Split('.')[0]);
            int minor = Convert.ToInt32(currentVersion.Split('.')[1]);
            int build = Convert.ToInt32(currentVersion.Split('.')[2]) + 1;


            PlayerSettings.bundleVersion = major + "." + minor + "." + build;

            if (buildTarget == BuildTarget.iOS)
            {
                PlayerSettings.iOS.buildNumber = "" + build + "";
                UnityEngine.Debug.Log("Finished with bundleversioncode:" + PlayerSettings.iOS.buildNumber + "and version" + PlayerSettings.bundleVersion);

            }
            else if (buildTarget == BuildTarget.Android)
            {
                PlayerSettings.Android.bundleVersionCode = build;
                UnityEngine.Debug.Log("Finished with bundleversioncode:" + PlayerSettings.Android.bundleVersionCode + "and version" + PlayerSettings.bundleVersion);
            }
            // It's important that you do not chane your project settings during a build in the cloud.


            // commit the settings to git only if you are in cloud build. If you save locally, we save your project settings so that you can commit them.
#if CLOUD_BUILD
			AssetDatabase.SaveAssets(); // should only be project version
			commitFileToGit ("ProjectSettings/ProjectSettings.asset");
#endif

        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
            UnityEngine.Debug.LogError("AutoIncrementBuildVersion script failed. Make sure your current bundle version is in the format X.X.X (e.g. 1.0.0) and not X.X (1.0) or X (1).");
        }
    }
}
#endif