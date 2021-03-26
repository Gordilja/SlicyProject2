using UnityEngine;

public class moveRightScript : MonoBehaviour
{
    private int force = 10;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveRight();

        if (transform.position.y < 1)
        {
            Destroy(gameObject);
        }
    }
    public void moveRight() 
    {
        rb.AddForce(Vector3.right * force);
    }
}
