using UnityEngine;

public class checkpoint2 : MonoBehaviour
{
    public CharacterController controller;

    void Start(){
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit Collision){

        if(Collision.gameObject.CompareTag("checkpoint") ){
            GameObject collided_checkpoint = Collision.gameObject;
            FindAnyObjectByType<GameManager2>().CheckPoint(collided_checkpoint);
            
        }
    }
}
