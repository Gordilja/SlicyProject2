using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(-12, 5, 5);

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
