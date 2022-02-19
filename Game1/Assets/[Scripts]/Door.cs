using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Animation Properties")] 
    public Animator animator;
   
    void Start()
    {
        animator = GetComponent<Animator>();
         animator.SetInteger("AnimationState", 0); //DoorSelected
    }

    public void OnDoor()
    {
        animator.SetInteger("AnimationState", 1); //DoorSelected
    }
}
