using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")]
    public float horizontalForce;
    public float verticalForce;  
    public float flyForce;     

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    [Header("Animation Properties")] 
    public Animator animator;

    [Header("Cameras")] 
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera doorCamera; 
    public CinemachineVirtualCamera spikesCamera; 
    
    [Header("UI Interactable")] 
    public GameObject doorSign;
    public bool isInteracting;
    
    private Rigidbody2D rigidBody2D;


    
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        doorSign.SetActive(false);

        Debug.Log("Scene Number: " + SceneManager.GetActiveScene().buildIndex);
    }

   
    void FixedUpdate()
    {        
        Move();    
        Interact();    
    }

    private void Interact()
    {
        isInteracting = Input.GetKey(KeyCode.E);
    }

    private void Move()
    {
    
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);
        float run = Input.GetAxisRaw("Horizontal"); //GetAxis sólo todos los valores entre -1 y 1. Raw, sólo -1,0 y 1.
        float jump = Input.GetAxisRaw("Jump"); //Jump 0 jump 1 con el Raw    
        float fly = Input.GetAxisRaw("Fly")*flyForce;
        if(fly > 0)
        {
            Vector2 move = new Vector2(run * horizontalForce, fly);
            rigidBody2D.AddForce(move);
        } else if (isGrounded)
        {
                      

            if (run != 0)
            {
                run = Flip(run);
                animator.SetInteger("AnimationState", 1); //Run State
            }
            else if (run == 0 && jump == 0)
            {
                animator.SetInteger("AnimationState", 0); //Idle State
            }

            if (jump > 0)
            {
                animator.SetInteger("AnimationState", 2); //Jump State
            } 
            else if (run == 0)
            {
                animator.SetInteger("AnimationState", 3); //Crocuh State
            }

            Vector2 move = new Vector2(run * horizontalForce, jump * verticalForce );
            rigidBody2D.AddForce(move);
                      
        }

    }

    private float Flip(float x)
    {
        x = (x > 0) ? 1 : -1;  //Ternary operator

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("DoorCamera"))
        {
            playerCamera.Priority = 5;
            doorCamera.Priority = 10;
            spikesCamera.Priority = 5;
            doorSign.SetActive(true);
        }

        if(other.gameObject.CompareTag("SpikesCamera"))
        {
            playerCamera.Priority = 5;
            doorCamera.Priority = 5;
            spikesCamera.Priority = 10;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("DoorCamera"))
        {
            playerCamera.Priority = 10;
            doorCamera.Priority = 5;
            doorSign.SetActive(false);
        }

        if(other.gameObject.CompareTag("SpikesCamera"))
        {
            playerCamera.Priority = 10;
            doorCamera.Priority = 5;
            spikesCamera.Priority = 5;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(isInteracting && other.gameObject.CompareTag("Door"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
}
