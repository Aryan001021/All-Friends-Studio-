using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PowerUpTimer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    [SerializeField] float timerLimit;
    float defaultTime;

    void Start()
    {
        defaultTime = timerLimit;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timerText.text ="Time:"+ ((int)timerLimit).ToString();
        timerLimit = (timerLimit - Time.deltaTime);

    }
    public float ReturnTimeLeft()
    {
        return timerLimit;
    }
    public void Resettimer()
    {
        timerLimit = defaultTime;
    }
}
