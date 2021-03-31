using System.Collections;
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
    public GameObject handle;
    //private bool hitHandle;

    // Start is called before the first frame update
    void Start()
    {
        blade.transform.gameObject.SetActive(false);
        handle.transform.gameObject.SetActive(false);
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
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Plane")
        {
            move = false;
            Debug.Log("Game Over");
            GetComponent<Animator>().enabled = false;
            rb.constraints = RigidbodyConstraints.None;
            FindObjectOfType<GameManager>().gameEnd();
        }
        else if (col.tag == "Slice")
        {
            StartCoroutine(slice());
        } 
        /*
        else if (col.tag == "Platform") 
        {
            stuck();
        }
        */

    }
    public IEnumerator stonk() 
    {
        GetComponent<Animator>().enabled = false;
        rb.detectCollisions = false;
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        rb.detectCollisions = true;
        this.GetComponent<Collider>().enabled = true;
    }
    public void stuck() 
    {
        //rb.isKinematic = true;
        animation.SetBool(isTapHash, false);
        blade.transform.gameObject.SetActive(true);
    }

    public void finish() 
    {
        move = false;
        Debug.Log("FINISH!");
        //handle.transform.gameObject.SetActive(true);
        GetComponent<Animator>().enabled = false;
        FindObjectOfType<GameManager>().nextlvl();
    }
    /*
    public IEnumerator hit() 
    {
        handle.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        handle.transform.gameObject.SetActive(false);
    }
    */
    IEnumerator slice()
    {
        blade.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        blade.transform.gameObject.SetActive(false);
    }

    void tapanim() 
    {
        FindObjectOfType<GameManager>().play();
        blade.transform.gameObject.SetActive(false);

        //Animation
        animation.SetBool(isTapHash, true);
        GetComponent<Animator>().enabled = true;
        //StartCoroutine(hit());

        //Movement
        rb.isKinematic = false;
        Vector3 temp = transform.position;
        temp.y += 0.5f;
        transform.position = temp;
        rb.AddForce(Vector3.back * force, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse); 
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
