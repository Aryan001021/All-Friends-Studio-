using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffFalling : MonoBehaviour
{
    bool isEnter = false;
    [SerializeField] GameObject platform;
    


    void Update()
    {
        if (isEnter == true)
        {
            Rigidbody2D fallingObject = platform.GetComponent<Rigidbody2D>();
            fallingObject.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter = true;
        }
    }
}
