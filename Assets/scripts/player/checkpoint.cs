using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public CharacterController controller;

    void Start(){
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit Collision){

        if(Collision.gameObject.CompareTag("checkpoint") ){
            GameObject collided_checkpoint = Collision.gameObject;
            FindAnyObjectByType<GameManager>().CheckPoint(collided_checkpoint);
            
        }
    }
}
