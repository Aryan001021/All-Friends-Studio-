using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class will set player prefs on first boot of the game
public class PrefrenceLoader : MonoBehaviour
{
    int firstBoot;
    void Awake()
    {
        if (PlayerPrefs.GetInt("firstBoot") == 0)
        {
            PlayerPrefs.SetInt("firstBoot", 1);
            PlayerPrefs.SetInt("fromLocal", 1);
            PlayerPrefs.SetInt("fromServer", 0);
            PlayerPrefs.SetInt("fromServerMajor", 0);
            PlayerPrefs.SetInt("fromServerMinor", 0);
            PlayerPrefs.SetInt("fromServerNew", 0);
        }
        else if(PlayerPrefs.GetInt("firstBoot") == 1)
        {
        }
    }
   
}
