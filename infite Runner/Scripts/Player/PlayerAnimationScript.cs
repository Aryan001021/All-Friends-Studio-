using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
   
    Animator playerAnimator;
    void Awake()
    {
        playerAnimator=GetComponentInChildren<Animator>();
    }
    
    public void ChangeAnimation(string animationName)
    {
        playerAnimator.Play(animationName);
    }
    
}
