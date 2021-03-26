using UnityEngine;

public class moveLeftScript : MonoBehaviour
{
    private int force = 15;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        moveLeft();

        if (transform.position.y < 1)
        {
            Destroy(gameObject);
        }
    }
    public void moveLeft()
    {
        rb.AddForce(Vector3.left * force);
    }
}
