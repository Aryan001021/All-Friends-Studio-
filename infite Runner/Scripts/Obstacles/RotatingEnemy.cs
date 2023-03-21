using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MonoBehaviour
{
    bool isEnter = false;
    [SerializeField] GameObject platform;
    private void Update()
    {
        if (isEnter)
        {
            platform.transform.Rotate(0, 0, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter= false;
        }
    }
}

