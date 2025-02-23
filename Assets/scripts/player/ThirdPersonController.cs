using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;


public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float jumpSpeed = 5f;
    public float gravity = -20f;
    public float turnSmoothTime = 0.1f;
    
    float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    
// Cinemachine.CinemachineVirtualCamera vCam; 

    PhotonView view;

    void Start(){
        view = GetComponent<PhotonView>();
        if (view.IsMine){
            cam = GetComponentInChildren<Camera>().transform;
            cam.tag = "MainCamera";
        }
    }

    void Update()
    {
        if(view.IsMine){

            // vCam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
            // if(vCam != null){
            //     vCam.Follow = transform;
            //     vCam.LookAt = transform;

            //     cam = vCam.transform;
            // }


            HandleMovement();    
        }
    }

    void HandleMovement(){
        isGrounded = controller.isGrounded;

            if (isGrounded && velocity.y < 0)
            {   
                velocity.y = -2f; // Reset velocity when grounded
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            bool jump = Input.GetButtonDown("Jump");
        
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, target_angle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            if (jump && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
    }
}
