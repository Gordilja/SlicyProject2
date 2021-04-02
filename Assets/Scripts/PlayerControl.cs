using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    int force = 3;
    int jumpforce = 24;
    float gravityModifier = 1.5f;
    bool move;
    private new Animator animation;
    int isTapHash;
    public GameObject blade;
    public GameObject handle;
    public GameObject player;
    public float rotationNum;
    public float rotationSet = 11;

    // Start is called before the first frame update
    void Start()
    {
        blade.transform.gameObject.SetActive(false);
        rotationNum = player.transform.eulerAngles.x;
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
            int touchNum = Input.touchCount;

            if (isClicked || touchNum > 0)
            {
                tapanim();
                //StartCoroutine(playerAnimation());
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

    IEnumerator playerAnimation() 
    {
        animation.enabled = true;
        //animation.SetBool(isTapHash, true);
        yield return new WaitForSeconds(0.61f);
        //animation.SetBool(isTapHash, false);
        animation.enabled = false;
    }

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

    public void finish() 
    {
        move = false;
        rb.isKinematic = true;
        Debug.Log("FINISH!");
        animation.enabled = false;
        FindObjectOfType<GameManager>().nextlvl();
    }


    IEnumerator slice()
    {
        blade.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        blade.transform.gameObject.SetActive(false);
        animation.SetBool(isTapHash, true);
    }

    void tapanim() 
    {
        FindObjectOfType<GameManager>().play();
        blade.transform.gameObject.SetActive(false);

        //Animation

        animation.SetBool(isTapHash, true);
        animation.enabled = true;
        //StartCoroutine(playerAnimation());

        /*
        if (rotationNum == rotationSet)
        {
            animation.SetBool(isTapHash, true);
            animation.enabled = true;
            StartCoroutine(playerAnimation());
        } 
        else if (rotationNum != rotationSet)
        {
            //animation.SetBool(isTapHash, true);
            animation.enabled = true;
            StartCoroutine(playerAnimation());
            player.transform.eulerAngles = new Vector3(11, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        }
       */

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

            if (touch.phase == TouchPhase.Moved)
            {
                tapanim();
            }
    }
    #endregion
}
