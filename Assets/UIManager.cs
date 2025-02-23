using UnityEngine;
using Photon.Pun;
using Unity.FPS.Gameplay;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel; // Assign your UI Canvas or Panel
    private GameObject localPlayer;
    public bool b;

     void Start()
    {   b=true;
        // Find the local player using Photon
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                localPlayer = player;
                break;
            }
        }
    }

    // private void FixedUpdate()
    // {
    //     if (uiPanel.activeSelf)
    //     {
    //         DisableLocalPlayer();
    //     }
    //     else
    //     {
    //         EnableLocalPlayer();
    //     }
    // }

    public void DisableLocalPlayer()
    {
        if ((localPlayer != null) && b )
        {
            localPlayer.GetComponent<PlayerInputHandler>().enabled = false;
            localPlayer.GetComponent<PlayerCharacterController>().enabled = false;
            // Unlock Cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            b=false;
            Debug.Log("Ho gya checkpoint" );
        }
    }

    public void EnableLocalPlayer()
    {
        if ((localPlayer != null) && b==false)
        {
            
            Debug.Log("Done bhai");
            // Lock Cursor for FPS
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            localPlayer.GetComponent<PlayerCharacterController>().enabled = true; 
            localPlayer.GetComponent<PlayerInputHandler>().enabled = true;
            b=true;
        }
    }
}
