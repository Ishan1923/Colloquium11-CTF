using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviourPun
{
    void Awake(){

        Spawn();
        ManageAudioListener();
    }

    public GameObject PlayerPrefab;
    public GameObject localplayerCamera;

    // public LayerMask terrainLayer;



    void ManageAudioListener(){
        Camera[] cameras = FindObjectsOfType<Camera>();


        foreach(Camera cam in cameras){

            PhotonView view = cam.GetComponent<PhotonView>();
            AudioListener listner = cam.GetComponent<AudioListener>();

            if(view != null && listner != null){
                
                if (view.IsMine){
                    listner.enabled = true;
                }
                else{
                    listner.enabled = false;
                }
            }
        }
    }
     GameObject player;
    GameObject localcam;

    public GameObject SpawnPoint;

    // public GameObject LocalPlayerCamera;

    // public float spawnRange = 10f;
    void Spawn(){
        // Vector3 randomPosition = new Vector3
        // (
        //     Random.Range(-spawnRange, spawnRange),
        //     0,
        //     Random.Range(-spawnRange, spawnRange)
        // );

        Vector3 spawnPointPos = SpawnPoint.transform.position;

        player = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPointPos , Quaternion.identity);
        // GameObject localcam = PhotonNetwork.Instantiate(localplayerCamera.name, spawnPointPos, Quaternion.identity);

        // if (player != null && player.GetComponent<ThirdPersonController>() != null)
        // {
        //     GrabCredentials controller = player.GetComponent<GrabCredentials>();

        //     if (controller.GetComponent<PhotonView>().IsMine)
        //     {
        //         //Assign the camera to the local player
        //         Camera mainCamera = localcam.GetComponentInChildren<Camera>();
        //         if (mainCamera != null)
        //         {
        //             mainCamera.tag = "MainCamera";  // Tag the camera as MainCamera for local player

        //             // Assign the camera transform to the ThirdPersonController
        //             controller.playerCharacterController.PlayerCamera = mainCamera;
        //         }

        //     }
        // }
        
        // if(player != null && player.GetComponentInChildren<Canvas>() != null){
        //     Canvas playerNameCanvas = player.GetComponentInChildren<Canvas>();
        //     DisplayToCamera script = playerNameCanvas.GetComponent<DisplayToCamera>();
        //     if(script != null){
        //         script.cam = localcam.transform;
        //     }
        //     else{
        //         Debug.Log("Not able to find Camera object in SpawnPlayer for DisplayToCamera script.");
        //     }

        // }
    }

    
}
