using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    const int SENSO_ORARIO = 0;
    const int SENSO_ANTIORARIO = 1;
    const float SWIPE_DISTANCE = 5f;

    private bool DesktopMode;
    private Vector2 startPos;
    private Vector2 endPos;
    private Touch touch;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Main.GamePaused && !Main.InputStopped)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                VerificaInputTouch();
            }
            else
            {
                VerificaInputMouse();
            }
        }
        // Enable pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !Main.GamePaused)
        {
            Main.GUI.ShowPauseMenu(true);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Caricatore"))
            {
                go.GetComponent<Rotore>().HitPoints = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Main.Level.InitLevelTimer(1);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Main.Player.AddSubCoins(1);
        }
    }

    private void VerificaInputMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPos = startPos;
        }
        //Verifico se è stato cliccato qualcosa con il tasto sinistro del mouse
        if (Input.GetMouseButtonUp(0))
        {

            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            VerificaCollisioni();
        }
    }

    private void VerificaInputTouch()
    {
        if (Input.touches.Length > 0)
        {
            touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startPos = Camera.main.ScreenToWorldPoint(touch.position);
                endPos = startPos;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endPos = Camera.main.ScreenToWorldPoint(touch.position);

                VerificaCollisioni();
            }
        }
    }

    private void VerificaCollisioni()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(startPos, endPos);

        foreach (RaycastHit2D hitInfo in hits)
        {
            if (hitInfo)
            {

                Rigidbody2D rb;

                rb = hitInfo.rigidbody;

                if (rb != null)
                {
                    float distance = Mathf.Max (Mathf.Abs(startPos.x - endPos.x), Mathf.Abs(startPos.y - endPos.y));

                    //if (rb.CompareTag("Caricatore") && startPos != endPos)
                    //{
                    //    VerificaRotazioneRotore(rb);
                    //}
                    if (rb.CompareTag("Caricatore") && distance <= 2)
                    {
                        VerificaRotazioneRotore(rb);
                    }


                    if (rb.CompareTag("TouchAreaPalline") && distance > 2)
                    {
                        //Ottengo lo stato di animazione del rotore
                        try
                        {
                            if (rb.transform.parent.parent.parent)
                            {
                                bool isclosing = rb.transform.parent.parent.parent.GetComponent<Rotore>().isClosing;

                                if (rb.transform.parent.parent && !isclosing)
                                {
                                    rb.transform.parent.parent.GetComponent<ScriptInnesto>().SganciaBall(rb.transform.parent);
                                }
                            }
                        }
                        catch
                        { }
                    }
                    if (rb.CompareTag("TouchAreaTuboScambio"))
                    {
                        rb.transform.Find("Freccia").GetComponent<FrecciaTuboScambio>().Rotate();
                    }
                }
            }

        }

        startPos = new Vector2(0, 0);
        endPos = new Vector2(0, 0);
    }

    private void VerificaRotazioneRotore(Rigidbody2D rb)
    {
        Rotore rotorescript = rb.transform.GetComponent<Rotore>();

        if (!rotorescript.IsRotating && !rotorescript.isClosing && !rotorescript.NewBallIncoming)
        {
            rotorescript.AvviaRotazione(SENSO_ORARIO);
        }
    }
}




#region Vecchio Programma
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerInputManager : MonoBehaviour
//{
//    const int SENSO_ORARIO = 0;
//    const int SENSO_ANTIORARIO = 1;
//    const float SWIPE_DISTANCE = 5f;

//    private bool DesktopMode;
//    private Vector2 startPos;
//    private Vector2 endPos;
//    private Touch touch;


//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!Main.GamePaused && !Main.InputStopped)
//        {
//            if (Application.platform == RuntimePlatform.Android)
//            {
//                VerificaInputTouch();
//            }
//            else
//            {
//                VerificaInputMouse();
//            }
//        }
//        // Enable pause menu
//        if (Input.GetKeyDown(KeyCode.Escape) && !Main.GamePaused)
//        {
//            Main.GUI.ShowPauseMenu(true);
//            Main.GamePaused = true;
//        }

//        // disable pause menu
//        else if (Input.GetKeyDown(KeyCode.Escape) && Main.GamePaused)
//        {
//            Main.GUI.ShowPauseMenu(false);
//            Main.GamePaused = false;
//        }

//        if (Input.GetKeyDown(KeyCode.W))
//        {
//            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Caricatore"))
//            {
//                go.GetComponent<Rotore>().HitPoints = 0;
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            Main.Level.InitLevelTimer(1);
//        }

//        if (Input.GetKeyDown(KeyCode.G))
//        {
//            Main.Player.AddSubCoins(1);
//        }
//    }

//    private void VerificaInputMouse()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            endPos = startPos;
//        }
//        //Verifico se è stato cliccato qualcosa con il tasto sinistro del mouse
//        if (Input.GetMouseButtonUp(0))
//        {

//            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//            VerificaCollisioni();
//        }
//    }

//    private void VerificaInputTouch()
//    {
//        if (Input.touches.Length > 0)
//        {
//            touch = Input.touches[0];

//            if (touch.phase == TouchPhase.Began)
//            {
//                startPos = Camera.main.ScreenToWorldPoint(touch.position);
//                endPos = startPos;
//            }

//            if (touch.phase == TouchPhase.Ended)
//            {
//                endPos = Camera.main.ScreenToWorldPoint(touch.position);

//                VerificaCollisioni();
//            }
//        }
//    }

//    private void VerificaCollisioni()
//    {
//        RaycastHit2D[] hits = Physics2D.LinecastAll(startPos, endPos);

//        foreach (RaycastHit2D hitInfo in hits)
//        {
//            if (hitInfo)
//            {

//                Rigidbody2D rb;

//                rb = hitInfo.rigidbody;

//                if (rb != null)
//                {
//                    float distance = Mathf.Abs(startPos.x - endPos.x);

//                    if (rb.CompareTag("Caricatore") && startPos != endPos)
//                    {
//                        VerificaRotazioneRotore(rb);
//                    }


//                    if (rb.CompareTag("TouchAreaPalline") && distance <= 2)
//                    {
//                        //Ottengo lo stato di animazione del rotore
//                        if (rb.transform.parent.parent.parent)
//                        {
//                            bool isclosing = rb.transform.parent.parent.parent.GetComponent<Rotore>().isClosing;

//                            if (rb.transform.parent.parent && !isclosing)
//                            {
//                                rb.transform.parent.parent.GetComponent<ScriptInnesto>().SganciaBall(rb.transform.parent);
//                            }
//                        }
//                    }
//                    if (rb.CompareTag("TouchAreaTuboScambio"))
//                    {
//                        rb.transform.Find("Freccia").GetComponent<FrecciaTuboScambio>().Rotate();
//                    }
//                }
//            }

//        }

//        startPos = new Vector2(0, 0);
//        endPos = new Vector2(0, 0);
//    }

//    private void VerificaRotazioneRotore(Rigidbody2D rb)
//    {
//        Rotore rotorescript = rb.transform.GetComponent<Rotore>();
//        float distance_x = Mathf.Abs(startPos.x - endPos.x);
//        float distance_y = Mathf.Abs(startPos.y - endPos.y);

//        if (!rotorescript.IsRotating && !rotorescript.isClosing && !rotorescript.NewBallIncoming)
//        {

//            if (distance_x > distance_y) //Ho fatto swipe più in orizzontale o in verticale?
//            {
//                //PRIMA L'ORIZZONTALE CON L'ACCORGIMENTO DI VERIFICARE DA CHE LATO HAI FATTO SWIPE ALTO O BASSO
//                if (startPos.x < endPos.x && distance_x >= SWIPE_DISTANCE)
//                {
//                    if (startPos.y > rb.transform.position.y)
//                    // Swipe in alto a destra
//                    {
//                        rotorescript.AvviaRotazione(SENSO_ORARIO);
//                    }
//                    else
//                    {
//                        rotorescript.AvviaRotazione(SENSO_ANTIORARIO);
//                    }
//                }
//                if (startPos.x > endPos.x && distance_x >= SWIPE_DISTANCE)
//                {
//                    if (startPos.y > rb.transform.position.y)
//                    //Swipe in alto a sinistra
//                    {
//                        rotorescript.AvviaRotazione(SENSO_ANTIORARIO);
//                    }
//                    else
//                    {
//                        rotorescript.AvviaRotazione(SENSO_ORARIO);
//                    }
//                }
//            }
//            else
//            {
//                //ORA IL VERTICALE CON L'ACCORGIMENTO DI VERIFICARE DA CHE LATO HAI FATTO SWIPE SINISTRO O DESTRO

//                if (startPos.y < endPos.y && distance_y >= SWIPE_DISTANCE)
//                {

//                    if (startPos.x < rb.transform.position.x)
//                    {
//                        //Swipe In alto a sinistra del rotore
//                        rotorescript.AvviaRotazione(SENSO_ORARIO);
//                    }
//                    else
//                    {
//                        rotorescript.AvviaRotazione(SENSO_ANTIORARIO);
//                    }
//                }
//                if (startPos.y > endPos.y && distance_y >= SWIPE_DISTANCE)
//                {
//                    if (startPos.x < rb.transform.position.x)
//                    {
//                        //Swipe In basso a sinistra del rotore
//                        rotorescript.AvviaRotazione(SENSO_ANTIORARIO);
//                    }
//                    else
//                    {
//                        rotorescript.AvviaRotazione(SENSO_ORARIO);
//                    }
//                }
//            }
//        }
//    }
//}
#endregion