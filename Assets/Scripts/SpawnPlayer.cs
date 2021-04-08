﻿using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 spawnPos;
    float positionY = 6.5f;
    float positionZ = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = new Vector3(0, positionY, positionZ);
        Instantiate(player, spawnPos, Quaternion.Euler(110, 180, 0));
    }
}