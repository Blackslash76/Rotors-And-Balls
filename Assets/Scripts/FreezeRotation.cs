using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {

    private Quaternion originalRotation;
    private Vector3 offset;


    // Use this for initialization
    void Start()
    {
        originalRotation = transform.rotation;
        offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation != originalRotation)
        {
            Vector3 newposition = new Vector3(transform.parent.position.x + offset.x, transform.parent.position.y + offset.y, transform.parent.position.z + offset.z);

            transform.position = newposition;
            transform.rotation = originalRotation;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MassAreaPalline"))
        {
            float otherBall_x = collision.transform.parent.GetComponent<BallManager>().directionX;
            float otherBall_y = collision.transform.parent.GetComponent<BallManager>().directionY;

            float directionX = transform.parent.GetComponent<BallManager>().directionX;
            float directionY = transform.parent.GetComponent<BallManager>().directionY;

            Debug.Log("X: " + directionX + " - " + otherBall_x + " <-> Y: " + directionY + " - " + otherBall_y);
            if (otherBall_x == directionX && otherBall_y == directionY)
            {
                if (directionX < otherBall_x)
                {
                    transform.parent.position=new Vector2(collision.transform.parent.position.x-10f,collision.transform.parent.position.y);
                }
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("MassAreaPalline"))
    //    {
    //        float otherBall_x = collision.transform.parent.GetComponent<BallManager>().directionX;
    //        float otherBall_y = collision.transform.parent.GetComponent<BallManager>().directionY;

    //        float directionX = transform.parent.GetComponent<BallManager>().directionX;
    //        float directionY = transform.parent.GetComponent<BallManager>().directionY;

    //        if (otherBall_x == directionX && otherBall_y == directionY)
    //        {
    //            collision.transform.parent.GetComponent<BallManager>().SetPauseBall(false);
    //        }
    //    }
    //}
}
