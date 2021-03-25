﻿using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    int force = 10;
    int jumpforce = 20;
    int i = 0;
    float gravityModifier = 2;

    Animator animation;
    int isTapHash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animation = GetComponent<Animator>();
        isTapHash = Animator.StringToHash("isTap");
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        bool isClicked = Input.GetKeyDown(KeyCode.Mouse0);
        bool isMoving = animation.GetBool(isTapHash);

        if (isClicked)
        {
            rb.isKinematic = false;
            //rb.AddTorque(0f, 0f, -1000, ForceMode.Impulse);
            rb.AddForce(Vector3.back * force, ForceMode.Impulse);
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            animation.SetBool(isTapHash, true);
        } 
        else if (!isClicked) 
        {
            animation.SetBool(isTapHash, false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Platform" && i < 1) 
        {
            rb.isKinematic = true;
            i++;
        }else if(i == 1) 
        {
            i--;
        }
           
    }
    #region 
    /*
    public void touchControl()
    {
        //Move to sides
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rb.AddTorque(0f, 0f, -1000, ForceMode.Impulse);
                rb.AddForce(Vector3.back * force, ForceMode.Impulse);
                rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            }
        }
    }
    */
    #endregion
}
