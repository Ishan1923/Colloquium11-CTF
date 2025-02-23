using UnityEngine;
using Photon.Pun;

public class RunnerMovement : MonoBehaviour
{
    public Animation anim;
    public Transform modelTransform;
    public float gravity = 20f;
    public CharacterController controller; // Player movement controller
    public float speed = 5f; // Movement speed
    public float rotationSpeed = 2f; // Mouse rotation speed
    private Vector2 rotation = Vector2.zero;
    private PhotonView view;
    public Camera cam;

    void Start()
    {
        view = GetComponent<PhotonView>();
        cam = GetComponentInChildren<Camera>();
        cam.enabled = view.IsMine;
    }

    void Update()
    {
        if (!view.IsMine) return;

        // Mouse rotation for looking around
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        rotation.y += mouseX;
        transform.localRotation = Quaternion.Euler(0, rotation.y, 0);

        // Movement using Arrow Keys
        float moveX = Input.GetAxis("Horizontal"); // Left (-1) & Right (1)
        float moveZ = Input.GetAxis("Vertical");   // Forward (1) & Backward (-1)

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        if (move.magnitude > 0.1f) // If moving
        {
            anim.Play();
            modelTransform.rotation = Quaternion.LookRotation(move); // Rotate model in movement direction
        }
        else
        {
            anim.Stop();
        }

        if (!controller.isGrounded)
        {
            move.y -= gravity * Time.deltaTime;
        }

        controller.Move(move * speed * Time.deltaTime);
    }
}
