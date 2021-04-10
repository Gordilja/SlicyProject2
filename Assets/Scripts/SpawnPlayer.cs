using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 spawnPos;
    float positionY = 6.5f;
    float positionZ = 6.0f;
    private void Start()
    {
        spawnPlayer();
    }

    public void spawnPlayer()
    {
        spawnPos = new Vector3(0, positionY, positionZ);
        Instantiate(player, spawnPos, Quaternion.Euler(110, 180, 0));
    }
    /*
    IEnumerator spawnMec() 
    {
        yield return new WaitForSeconds(0.2f);
        spawnPos = new Vector3(0, positionY, positionZ);
        Instantiate(player, spawnPos, Quaternion.Euler(110, 180, 0));
    }
    */
}
