using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreenHandler : MonoBehaviour
{
    private void  MainMenuLoader()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void Start()
    {
        MainMenuLoader();
        
    }

    //for video play 
    //[SerializeField]VideoPlayer vPlayer;
    //bool startPlaying = true;
    //void Awake()
    //{

    //    vPlayer.Play();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (vPlayer.isPlaying) {
    //        startPlaying= false;
    //    }        
    //    if(startPlaying==false) 
    //    {if (vPlayer.isPlaying == false) {MainMenuLoader(); }
    //    }
    //}
}
