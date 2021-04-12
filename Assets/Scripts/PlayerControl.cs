using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Movement
    public Rigidbody rb;
    int force = 10;
    int jumpforce = 15;
    float gravityModifier = 1;
    bool move;
    //Animation
    private new Animator animation;
    int isTapHash;
    //Objects
    public GameObject blade;
    public GameObject handle;

    // Start is called before the first frame update
    void Start()
    {
        blade.transform.gameObject.SetActive(false);
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
        //Controls
        if (move == true) 
        {
            bool isClicked = Input.GetKeyDown(KeyCode.Mouse0);
            int touchNum = Input.touchCount;

            if (isClicked)
            {
                tapanim();
            } 
            else if (touchNum > 0) 
            {
                touchControl();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Plane")
        {
            move = false;
            Debug.Log("Game Over");
            animation.enabled = false;
            rb.constraints = RigidbodyConstraints.None;
            FindObjectOfType<GameManager>().gameEnd();
        }
        else if (col.tag == "Slice")
        {
            StartCoroutine(slice());
        }
        else if (col.tag == "Platform" || col.tag == "Finish")
        {
            blade.transform.gameObject.SetActive(true);
        }

    }
    #region Platform collission action
    public IEnumerator stonk() 
    {
        rb.isKinematic = true;
        animation.enabled = false;
        rb.detectCollisions = false;
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        rb.detectCollisions = true;
        this.GetComponent<Collider>().enabled = true;
    }

    public void stuck() 
    {
        rb.isKinematic = true;
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
        StartCoroutine(stonk());
    }
    #endregion

    //Hit finish
    public void finish() 
    {
        move = false;
        rb.isKinematic = true;
        Debug.Log("FINISH!");
        animation.enabled = false;
        FindObjectOfType<GameManager>().nextlvl();
    }


    //Slice object
    IEnumerator slice()
    {
        blade.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        blade.transform.gameObject.SetActive(false);
        animation.SetBool(isTapHash, true);
    }

    //Animation on controls
    void tapanim() 
    {
        FindObjectOfType<GameManager>().play();
        blade.transform.gameObject.SetActive(false);

        //Animation
        animation.SetBool(isTapHash, true);
        animation.enabled = true;

        //Movement
        rb.isKinematic = false;
        Vector3 temp = transform.position;
        temp.y += 0.5f;
        transform.position = temp;
        rb.AddForce(Vector3.back * force, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse); 
    }

    #region TouchControls
    public void touchControl()
    {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                tapanim();
            }
    }
    #endregion
}
