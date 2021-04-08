using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera camera;
    public Transform target;
    private Vector3 offset = new Vector3(-6, 3, 5);

    void Start()
    {
        camera = GetComponent<Camera>();
        GetTargetByTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetTargetByTag("Player");
        if (target)
        {
            transform.position = target.transform.position + offset; 
        }

    }

    /// Changes the target.
    void ChangeTarget(Transform _target)
    {
        target = _target;
    }

    /// Gets the target by tag.
    void GetTargetByTag(string _tag)
    {
        GameObject obj = GameObject.FindWithTag(_tag);
        if (obj)
        {
            ChangeTarget(obj.transform);
        }
        else
        {
            Debug.Log("Cant find object with tag " + _tag);
        }
    }
}
