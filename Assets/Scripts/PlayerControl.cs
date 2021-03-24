using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    int force = 100;
    int jumpforce = 150;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            /*
            rb.AddTorque(0f, 0f, -500, ForceMode.Impulse);
            rb.AddForce(Vector3.back * force);
            rb.AddForce(Vector3.up * jumpforce);
            */

            rb.isKinematic = false;
            rb.AddForceAtPosition(Vector3.up * 10, Vector3.forward * force);
            rb.AddForce(Vector3.back * force);
            rb.AddForce(Vector3.up * jumpforce);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //if(col.tag == "Platform")
            //rb.isKinematic = true;
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
                rb.isKinematic = false;
                rb.AddForceAtPosition(Vector3.up * 10, Vector3.forward * force);
                rb.AddForce(Vector3.back * force);
                rb.AddForce(Vector3.up * jumpforce);
            }
        }
    }
    */
}
