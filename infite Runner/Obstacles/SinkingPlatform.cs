using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingPlatform : MonoBehaviour
{
    bool isCollided = false;
    Rigidbody2D platformrigidBody;
    private void Awake()
    {
        platformrigidBody= GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        platformrigidBody.bodyType = RigidbodyType2D.Dynamic;
        if (collision.gameObject.tag == "deathbed")
        {
            Destroy(gameObject);
        }
    }
}
