using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] string powerUpType;
    public string ReturnPowerUpType()
    {
        return powerUpType;
    }
}
