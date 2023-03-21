using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectDestroyer : MonoBehaviour
{
    [SerializeField] GameObject parentGameObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Platform")
        {
            Destroy(parentGameObject);
        }
    }
}
