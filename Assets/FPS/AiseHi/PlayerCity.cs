using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCity : MonoBehaviour
{
    public float gravity = 20f;
    public float moveSpeed = 10f;
    public float walkSpeed = 10f;
    public float runSpeed;
    public float smoothTime=0.1f;
    public Animator animator;
    public CharacterController player;
    public float mouseSens;
    Vector3 rotation = Vector3.zero;
    public float smoothMouseX;
    public Transform tr;
    private bool isCursorLocked = true;
    public float rotationSpeed=3f;
    public Transform body;
    private bool back=true;
    public float currentSpeed;
    public bool walk =false;
    public bool run=false;
    public float mouse=4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float z=Input.GetAxis("Vertical");
        if(z < 0)
          {
            walk=true;
            if(back){
            body.Rotate(new Vector3(0,180f,0),Space.Self);
            back = false;
            }
           }
        else if(z > 0)
        {
                walk=true;
                if(!back){
                body.Rotate(new Vector3(0,180,0),Space.Self);
                back = true;
                }
           
        }
        else{
            walk=false;
        }
        animator.SetBool("Walk",walk);
        // HandleMouseLock();
        if (isCursorLocked)
        {
        // float x=Input.GetAxis("Horizontal");
        

        Vector3 move = transform.forward*z ;
        // if(!run)
        // currentSpeed = move.magnitude;
        // else
        // currentSpeed = move.magnitude+1;

        if (!player.isGrounded)
            {
                move.y -= gravity * Time.deltaTime;
            }

        // Update animator parameter
        animator.SetFloat("Speed", currentSpeed);
        // if (move.magnitude > 0.1f)
        // {
        //     // Normalize the movement vector to prevent fast diagonal movement
        //     move.Normalize();

        //     // Move the character
        //     // transform.Translate(movement * speed * Time.deltaTime, Space.World);

        //     // Rotate the character to face the movement direction
        //     Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        // }


        float mouseX= Input.GetAxis("Mouse X")* mouse * mouseSens*Time.deltaTime ;
        smoothMouseX = Mathf.Lerp(smoothMouseX, mouseX, smoothTime);
        tr.Rotate(Vector3.up*smoothMouseX);
        
        player.Move(move*moveSpeed*Time.deltaTime);

        
        
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            
            Run();
            
        }
        else{ 
            run=false; 
        moveSpeed=walkSpeed;}
        // rotation.y += x;
        // transform.localRotation = Quaternion.Euler(rotation.x * .2f * Time.deltaTime *8f, rotation.y * .2f * Time.deltaTime*8f, 0);
        }
        animator.SetBool("Run",run);
    }
    public void Run()
    {   
        run=true;
        moveSpeed = runSpeed;
    }
    private void HandleMouseLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;

            if (isCursorLocked)
            {
                LockCursor();
            }
            else
            {
                UnlockCursor();
            }
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
    }
}

