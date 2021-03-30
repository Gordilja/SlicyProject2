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
    public GameObject blade;
    //public GameObject tip;

    // Start is called before the first frame update
    void Start()
    {
        blade.transform.gameObject.SetActive(false);
        //tip.transform.gameObject.SetActive(false);
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
                tapanim();
            }
            /*
            else if (!isClicked)
            {
                animation.SetBool(isTapHash, false);
            }
            */
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Platform")
        {
            StartCoroutine(stonk());
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
        else if (col.tag == "Slice") 
        {
            StartCoroutine(slice());
        }

    }
    IEnumerator stonk() 
    {
        rb.isKinematic = true;
        animation.SetBool(isTapHash, false);
        GetComponent<Animator>().enabled = false;
        rb.detectCollisions = false;
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        rb.detectCollisions = true;
        this.GetComponent<Collider>().enabled = true;
    }

    IEnumerator slice()
    {
        blade.transform.gameObject.SetActive(true);
        //tip.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        blade.transform.gameObject.SetActive(false);
        //tip.transform.gameObject.SetActive(false);
    }

    void tapanim() 
    {
        FindObjectOfType<GameManager>().play();

        //Animation
        animation.SetBool(isTapHash, true);
        GetComponent<Animator>().enabled = true;

        //Movement
        rb.isKinematic = false;
        Vector3 temp = transform.position;
        temp.y += 0.5f;
        transform.position = temp;
        rb.AddForce(Vector3.back * force, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

        //Proba
     
    }
    #region TouchControls
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
