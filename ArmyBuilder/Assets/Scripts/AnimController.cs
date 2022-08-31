using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
       animator=GetComponent<Animator>(); 
    }

    public void startWalking()
    {
        animator.SetBool("Walk", true);
    }
    public void stopWalking()
    {
        animator.SetBool("Walk", false);
    }
    public void PlayCheer()
    {
        animator.Play("Cheer");
    }
}
