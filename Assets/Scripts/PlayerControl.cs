using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    int force = 3;
    int jumpforce = 15;
    float gravityModifier = 2;
    bool move;

    Animator animation;
    int isTapHash;

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
                GetComponent<Animator>().enabled = true;
                rb.isKinematic = false;
                rb.AddForce(Vector3.back * force, ForceMode.Impulse);
                rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

                animation.SetBool(isTapHash, true);
            }
            else if (!isClicked)
            {
                //GetComponent<Animator>().enabled = false;
            }
        }
      
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Platform")
        {
            rb.isKinematic = true;
            animation.SetBool(isTapHash, false);
            GetComponent<Animator>().enabled = false;
        }
        else if (col.tag == "Plane") 
        {
            move = false;
            Debug.Log("Game Over");
            GetComponent<Animator>().enabled = false;
            rb.constraints = RigidbodyConstraints.None;
        }
        else if (col.tag == "Finish")
        {
            rb.isKinematic = true;
            move = false;
            Debug.Log("FINISH!");
            GetComponent<Animator>().enabled = false;
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
