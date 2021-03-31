using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Platform")
        {
            FindObjectOfType<PlayerControl>().stuck();
            player.AddComponent<Rigidbody>().isKinematic = true;
            FindObjectOfType<PlayerControl>().stonk();
        }
        else if (col.tag == "Finish") 
        {
            FindObjectOfType<PlayerControl>().finish();
        }
    }
}
