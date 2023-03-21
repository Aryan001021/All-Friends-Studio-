using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBoundary : MonoBehaviour
{
    GameObject player;
    bool canMoveNow=false;
    int distancePlatformAway = 80;
    float playerPosX;

    private void Awake()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            print("Player not found");
        }
        transform.position = new Vector3(player.transform.position.x, 0, 0);
        playerPosX = player.transform.position.x;
    }
    void Update()
    {
        if (player.transform.position.x-transform.position.x> distancePlatformAway) 
        {
            canMoveNow=true;
        }
        if (player.transform.position.x > playerPosX)
        {
            playerPosX= player.transform.position.x;
        }
        if (canMoveNow)
        {

            transform.position = new Vector3(playerPosX-distancePlatformAway, 0f, 0f);
        }
    }
}
