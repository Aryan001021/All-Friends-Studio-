using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gate : MonoBehaviour
{
    public event EventHandler OnGateEntered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnGateEntered?.Invoke(this,EventArgs.Empty);
        }
    }
}
