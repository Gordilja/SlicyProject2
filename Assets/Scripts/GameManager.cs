using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    bool start;

    // Start is called before the first frame update
    void Start()
    {
        //if (start == true)
            //player.transform.Translate(Vector3.forward * Time.deltaTime * 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
                start = true;
    }
}
