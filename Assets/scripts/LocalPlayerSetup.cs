using UnityEngine;
using Photon.Pun;
public class LocalPlayerSetup : MonoBehaviourPun
{
    PhotonView view;
    AudioListener listner;

    void Start(){
        view = GetComponent<PhotonView>();
        listner = GetComponent<AudioListener>();
        if(listner!=null)
        listner.enabled = view.IsMine;
        

    }
}
