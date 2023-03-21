using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetection : MonoBehaviour
{
    BoxCollider2D playerDetectionZone; 
    private bool isPlayerInside=false;
    void Awake()
    {
        playerDetectionZone = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            isPlayerInside=false;
        }
    }
    public bool PlayerInsideEnemyzone()
    {
        return isPlayerInside;
    }
    
}
