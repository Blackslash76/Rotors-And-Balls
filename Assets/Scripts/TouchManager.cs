using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    //private Vector2 startPos;
    //private Vector2 endPos;
    //private Vector2 touchPos;
    //private Rigidbody rb;
    //private Touch touch;
    //private bool DesktopMode = true;

    //private GlobalVariables main;
    //void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    main = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
        
    //}

    //void FixedUpdate()
    //{
    //    if (!DesktopMode)
    //    {
    //        CheckTouch();
    //    }
    //    else
    //    {
    //        CheckMouse();
    //    }


    //}


    //private void CheckMouse()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // get the x and y coordinates of screen touch
    //        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        // set movement to 0
            
    //        main.SetDebugText("Touch Mouse Started");

    //    }
    //    // Check if user removes finger from screen
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        // get last finger touch coordinates
    //        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //        Debug.Log(startPos + " - " + endPos);

    //        // record time it took to finish finger swipe
    //        //speed = Time.time - speed;
    //        // set movement to end touch minus begin touch for both x and y axis


    //        //**************Qua devo intervenire io*******************
    //        //******* DEVO CREARE UN RAYCAST *************
    //        RaycastHit2D[] hits;
    //        hits = Physics2D.LinecastAll(startPos, endPos);

    //        foreach (RaycastHit2D hitInfo in hits)
    //        {
    //            if (hitInfo)
    //            {
    //                Debug.Log(hitInfo.transform.name);
    //            }
    //            //moveHorizontal = endPos.x - startPos.x;
    //            //moveVertical = endPos.y - startPos.y;
    //            //// create movement variable
    //            //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //            //// add force to object
    //            //rb.AddForce(movement * speed);
    //        }
    //    }
    //}
    //private void CheckTouch()
    //{
    //    float moveHorizontal;
    //    float moveVertical;

    //    if (Input.touches.Length > 0)
    //    {
    //        touch = Input.touches[0];

    //        // Check if user touches screen
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            // get the x and y coordinates of screen touch
    //            startPos = touch.position;
    //            endPos = touch.position;
    //            // set movement to 0
    //            moveHorizontal = 0.0f;
    //            moveVertical = 0.0f;
    //            main.SetDebugText("Touch Started");

    //        }
    //        // Check if user removes finger from screen
    //        if (touch.phase == TouchPhase.Ended)
    //        {
    //            // get last finger touch coordinates
    //            endPos = touch.position;
    //            // record time it took to finish finger swipe
    //            //speed = Time.time - speed;
    //            // set movement to end touch minus begin touch for both x and y axis


    //            //**************Qua devo intervenire io*******************
    //            //******* DEVO CREARE UN RAYCAST *************
    //            RaycastHit2D[] hits;
    //            hits = Physics2D.LinecastAll(startPos, endPos, LayerMask.NameToLayer("Touch Layer"));



    //            foreach (RaycastHit2D hitInfo in hits)
    //            {
    //                if (hitInfo)
    //                {
    //                    main.SetDebugText(hitInfo.transform.name);
    //                }
    //                //moveHorizontal = endPos.x - startPos.x;
    //                //moveVertical = endPos.y - startPos.y;
    //                //// create movement variable
    //                //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //                //// add force to object
    //                //rb.AddForce(movement * speed);
    //            }
    //        }
    //    }

    //}
}