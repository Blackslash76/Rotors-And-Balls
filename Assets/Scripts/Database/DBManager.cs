using UnityEngine;
using System.Collections;

public class DBManager : MonoBehaviour
{
    private string secretKey = "GBsCFLwFXRt2NJkL"; // Edit this value and make sure it's the same as the one stored on the server
    public string addScoreURL = "http://www.centroufficio.net/php/addscore.php?"; //be sure to add a ? to your url
    public string highscoreURL = "http://www.centroufficio.net/php/displayscore.php?";
    public string newguidURL = "http://www.centroufficio.net/php/newguid.php?";
    public string updateguidURL = "http://www.centroufficio.net/php/updateguid.php?";
    public string addstatURL = "http://www.centroufficio.net/php/addstat.php?";


    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
    void Start()


    {
    }

    public void InserisciPunteggio(string guid, int levelnumber, int difficulty, int score)
    {
        StartCoroutine(PostScores(guid, levelnumber, difficulty, score));
    }

    public void OttieniPunteggio(int levelnumber, int difficulty)
    {
        StartCoroutine(GetScores(levelnumber, difficulty));
    }

    public void InserisciStat(int levelnumber, int difficulty, int timeleft)
    {
        StartCoroutine(PostStat(levelnumber, difficulty, timeleft));
    }

    public void VerificaGUID()
    {
        if (Main.UID == null)
        {
            StartCoroutine(GenerateNewGUID());
        }
        else
        {
            if (Main.UID.Length != 38)
            {
                StartCoroutine(GenerateNewGUID());
            }
            else
            {
                AggiornaGUID();
            }
        }
    }

    public void AggiornaGUID()
    {
        StartCoroutine(UpdateGUID());
    }

    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores(string guid, int levelnumber, int difficulty, int score)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(guid + levelnumber + difficulty + score + secretKey);

        string post_url = addScoreURL + "guid=" + WWW.EscapeURL(guid) + "&score=" + score + "&levelnumber=" + levelnumber + "&difficulty=" + difficulty + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
        else
        {
            print(post_url);
        }
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores(int levelnumber, int difficulty)
    {
        WWW hs_get = new WWW(highscoreURL + "levelnumber=" + levelnumber + "&difficulty=" + difficulty + "&limit=1");
        yield return hs_get;

        Debug.Log(hs_get.url);
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
            Main.Level.InternetScores.Nome = "";
            Main.Level.InternetScores.Punteggio = "";
        }
        else
        {
            if (hs_get.text != "")
            {
                string[] data = hs_get.text.Split('\t');

                Main.Level.InternetScores.Nome = data[0];
                Main.Level.InternetScores.Punteggio = data[1];
                Main.Level.InternetScores.Icona = int.Parse(data[2]);
                Main.Level.InternetScores.Guid = data[3];

                Debug.Log(data[3] + " - " + Main.UID);
            }
            else
            {
                Main.Level.InternetScores.Nome = "";
                Main.Level.InternetScores.Punteggio = "";
            }
        }
    }

    IEnumerator GenerateNewGUID()
    {
        if (Main.Player.PlayerName == "")
        {
            Main.Player.PlayerName= "Player" + Random.Range(1, 100000);
        }

        string hash = Md5Sum(Main.Player.PlayerName + secretKey);

        string post_url = newguidURL + "name=" + WWW.EscapeURL(Main.Player.PlayerName) + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        print(post_url);
        if (hs_post.error != null)
        {
            print("There was an error with GUID Generation: " + hs_post.error);
        }
        else
        {
            Debug.Log("LUNGHEZZE UID: " + hs_post.text.Length);
            if (hs_post.text.Length == 38)
            {
                Main.UID = hs_post.text;
                Debug.Log("NUOVO GUID:" + Main.UID);
            }
        }

    }

    IEnumerator UpdateGUID()
    {
        string name = Main.Player.PlayerName;
        if (name=="")
        {
            name = "Player" + Random.Range(1, 100000);
        }

        string hash = Md5Sum(name + Main.Player.PlayerIcon + Main.UID + secretKey);
        string post_url = updateguidURL + "name=" + WWW.EscapeURL(name) + "&icon=" + Main.Player.PlayerIcon + "&GUID=" + WWW.EscapeURL(Main.UID) + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        print(post_url);
        if (hs_post.error != null)
        {
            print("There was an error with GUID Generation: " + hs_post.error);
        }
    }
    IEnumerator PostStat(int levelnumber, int difficulty, int timeleft)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(levelnumber + difficulty + timeleft + secretKey);

        string post_url = addstatURL + "levelnumber=" + levelnumber + "&difficulty=" + difficulty + "&timeleft=" + timeleft + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
        else
        {
            print(post_url);
        }
    }



    private string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}