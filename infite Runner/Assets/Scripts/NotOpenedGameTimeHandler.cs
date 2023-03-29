using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotOpenedGameTimeHandler : MonoBehaviour
{
    DateTime sessionEnd = DateTime.UtcNow;
    int openAfterDays;
    int openAfteHours;
    int openAfterMinutes;
    int openAfterSeconds;
    int seconds;
    private void Awake()
    {
        openAfterDays = sessionEnd.Day;
        openAfteHours=sessionEnd.Hour;
        openAfterMinutes=sessionEnd.Minute;
        openAfterSeconds=sessionEnd.Second;
        if (openAfterDays < PlayerPrefs.GetInt("endTimeDays"))
        {
            print("take all the coin");
        }
        else
        {
            seconds = ((openAfterDays - PlayerPrefs.GetInt("endTimeDays"))*24*60*60) +
                ((openAfteHours - PlayerPrefs.GetInt("endTimeHour"))*60*60) +
                ((openAfterMinutes - PlayerPrefs.GetInt("endTimeMin"))*60)+
                (openAfterSeconds-PlayerPrefs.GetInt("endTimeSec"));
        }
        print("Seconds:"+seconds);
        
    }
    private void OnApplicationQuit()
    {
       PlayerPrefs.SetInt("endTimeDays",sessionEnd.Day);
       PlayerPrefs.SetInt("endTimeHour",sessionEnd.Hour);
       PlayerPrefs.SetInt("endTimeMin", sessionEnd.Minute);
       PlayerPrefs.SetInt("endTimeSec", sessionEnd.Second);
    }

}
