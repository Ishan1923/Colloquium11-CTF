using Cinemachine;
using UnityEngine;
using Photon.Pun;
using System.Collections;

public class GetPlayerTransform : MonoBehaviourPun
{

    CinemachineFreeLook vCam;

    void Start()
    {
        vCam = GetComponent<CinemachineFreeLook>();

        if(vCam == null){
            Debug.Log("CinemachineFreeLook Component Not Found");
            return;
        }

        StartCoroutine(FindLocalPlayer());

    }

    private IEnumerator FindLocalPlayer(){
        GameObject player = null;

        while (player == null){
            player = FindLocalPlayerObject();
            yield return null;
        }

        vCam.Follow = player.transform;
        vCam.LookAt = player.transform;
    }

    private GameObject FindLocalPlayerObject(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players){
            PhotonView photonView = player.GetComponent<PhotonView>();
            if(photonView.IsMine && photonView != null){
                return player;
            }
        }

        return null;
    }

}
