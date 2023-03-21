using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffDestroyer : MonoBehaviour
{
    GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            print("find not Player");
        }
        
    }
    void Update()
    {
        float playerPos = player.transform.position.x; 
        if (transform.position.x < playerPos - 100)
        {
            Destroy(gameObject);
        }
    }
}
