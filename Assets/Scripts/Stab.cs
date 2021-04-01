using UnityEngine;

public class Stab : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Platform")
        {
            FindObjectOfType<PlayerControl>().stuck();
        }
        else if (col.tag == "Finish") 
        {
            FindObjectOfType<PlayerControl>().finish();
        }
    }
}
