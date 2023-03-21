using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    bool isEnter = false;
    bool canShift = false;
    int count = 0;
    [SerializeField] GameObject platform;
    private void Update()
    {
        if (isEnter)
        {
            if (count < 180 && canShift==false)
            {
                count++;
                platform.transform.Rotate(0, 0, 1);
            }
            else 
            {
                canShift= true;
                count--;
                platform.transform.Rotate(0, 0, -1);
                if (count==0)
                {
                    canShift=false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter = false;
        }
    }
}
