using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class CheckDestroy : MonoBehaviour
{
    PhotonView photonView;
    public GameObject[] gameObjects;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        photonView=GetComponent<PhotonView>();
        foreach(GameObject gameObj in gameObjects){
        pv = gameObj.AddComponent<PhotonView>();
        pv.ViewID = PhotonNetwork.AllocateViewID(true); // Assign a unique ViewID
        // gameObj.AddComponent<CheckDestroy>();
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DestroyIt()
    {
        // photonView=GetComponent<PhotonView>();
      photonView.RPC("KillIt",RpcTarget.MasterClient);
      
    }
    [PunRPC] public void KillIt()
    {
        
        if (PhotonNetwork.IsMasterClient)
        {
            foreach(GameObject gameObj in gameObjects)
            {
                // gameObj.GetComponent<CheckDestroy>().DestroyIt();
            PhotonNetwork.Destroy(gameObj);
            }
            
            // Master Client destroys the object across the network
            PhotonNetwork.Destroy(gameObject);
            
        }
    }
}
