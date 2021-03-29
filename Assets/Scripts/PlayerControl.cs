﻿using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    int force = 3;
    int jumpforce = 17;
    float gravityModifier = 2;
    bool move;
    private new Animator animation;
    int isTapHash;
    public GameObject knife;
    // Start is called before the first frame update
    void Start()
    {
        move = true;
        rb = GetComponent<Rigidbody>();
        animation = GetComponent<Animator>();
        isTapHash = Animator.StringToHash("isTap");
        Physics.gravity *= gravityModifier;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true) 
        {
            bool isClicked = Input.GetKeyDown(KeyCode.Mouse0);
            bool isMoving = animation.GetBool(isTapHash);

            if (isClicked)
            {
                knife.GetComponent<Collider>().enabled = true;
                rb.isKinematic = false;
                animation.SetBool(isTapHash, true);
                GetComponent<Animator>().enabled = true;
                FindObjectOfType<GameManager>().play();
                //GetComponent<Animator>().enabled = true;
                //rb.isKinematic = false;
                Vector3 temp = transform.position;
                temp.y += 0.5f;
                transform.position = temp;
                rb.AddForce(Vector3.back * force, ForceMode.Impulse);
                rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

                animation.SetBool(isTapHash, true);
            }
            else if (!isClicked)
            {
               // animation.SetBool(isTapHash, false);
            }
        }
      
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Platform")
        {   
            StartCoroutine(delay());
        }
        else if (col.tag == "Plane") 
        {
            move = false;
            Debug.Log("Game Over");
            GetComponent<Animator>().enabled = false;
            rb.constraints = RigidbodyConstraints.None;
            FindObjectOfType<GameManager>().gameEnd();
        }
        else if (col.tag == "Finish")
        {
            rb.isKinematic = true;
            move = false;
            Debug.Log("FINISH!");
            GetComponent<Animator>().enabled = false;
            FindObjectOfType<GameManager>().nextlvl();
        }
       
    }

    IEnumerator delay() 
    {
        rb.isKinematic = true;
        animation.SetBool(isTapHash, false);
        GetComponent<Animator>().enabled = false;
        knife.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2);
      
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
                rb.isKinematic = false;
                //rb.AddTorque(0f, 0f, -1000, ForceMode.Impulse);
                rb.AddForce(Vector3.back * force, ForceMode.Impulse);
                rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
                animation.SetBool(isTapHash, true);
            }
        }
    }
    */
    #endregion
}
