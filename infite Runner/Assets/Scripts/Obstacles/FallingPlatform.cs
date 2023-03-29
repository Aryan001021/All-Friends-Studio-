using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isEnter=false;
    [SerializeField]GameObject platform;
    
   
    void Update()
    {
        if (isEnter==true)
        {
            platform.transform.position-=new Vector3(0,1,0);
        }     
        if (platform.transform.position.y <-100)
        {
            Destroy(gameObject);
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
