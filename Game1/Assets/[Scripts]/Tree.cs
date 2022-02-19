using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Animation Properties")] 
    public Animator animator;
   
    void Start()
    {
        animator = GetComponent<Animator>();
         animator.SetInteger("AnimationState", 0); //DoorSelected
    }

    public void OnTree()
    {
        animator.SetInteger("AnimationState", 1); //DoorSelected
    }

    public void OffTree()
    {
        animator.SetInteger("AnimationState", 0); //DoorSelected
    }
}

