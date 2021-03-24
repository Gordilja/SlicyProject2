﻿using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rigidbody;
    int force = 100;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            rigidbody.AddForceAtPosition(Vector3.up * 10, Vector3.forward * force);
            rigidbody.AddForce(Vector3.back * 100);
            //transform.Translate(Vector3.forward * Time.deltaTime * 10);
        }
    }
    /*
    public void touchControl()
    {
        //Move to sides
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotateY = Quaternion.Euler(0f, touch.deltaPosition.x * rotatespeed, 0f);
                transform.rotation *= rotateY;
            }
        }
    }
    */
}