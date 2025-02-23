using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class SpawnPlayer2 : MonoBehaviourPun
{

    public GameObject PlayerPrefab;
    public GameObject localplayerCamera;
    public Camera localcam;


    // public LayerMask terrainLayer;



    // void ManageAudioListener(GameObject LocalCamera){
    //     Camera[] cameras = FindObjectsOfType<Camera>();


    //     foreach(Camera cam in cameras){

    //         PhotonView view = cam.GetComponent<PhotonView>();
    //         AudioListener listner = cam.GetComponent<AudioListener>();

    //         if(view != null && listner != null){
                
    //             if (view.IsMine){
    //                 listner.enabled = true;
    //             }
    //             else{
    //                 listner.enabled = false;
    //             }
    //         }
    //     }
    // }

    public GameObject SpawnPoint;

    // public GameObject LocalPlayerCamera;

    // public float spawnRange = 10f;
    public void Start(){
        Spawn();
        localcam = FindAnyObjectByType<Camera>();
        
    }
    void Spawn(){
        // Vector3 randomPosition = new Vector3
        // (
        //     Random.Range(-spawnRange, spawnRange),
        //     0,
        //     Random.Range(-spawnRange, spawnRange)
        // );

        Vector3 spawnPointPos = SpawnPoint.transform.position;

        GameObject player = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPointPos, Quaternion.identity);
        // GameObject localcam = PhotonNetwork.Instantiate(localplayerCamera.name, spawnPointPos, Quaternion.identity);

        // if (player != null && player.GetComponent<ThirdPersonController>() != null)
        // {
        //     ThirdPersonController controller = player.GetComponent<ThirdPersonController>();

        //     if (controller.GetComponent<PhotonView>().IsMine)
        //     {
        //         // Assign the camera to the local player
        //         Camera mainCamera = localcam.GetComponentInChildren<Camera>();
        //         if (mainCamera != null)
        //         {
        //             mainCamera.tag = "MainCamera";  // Tag the camera as MainCamera for local player

        //             // Assign the camera transform to the ThirdPersonController
        //             controller.cam = mainCamera.transform;

        //             player.GetComponentInChildren<Canvas>().GetComponent<DisplayToCamera>().cam = mainCamera.transform;
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
