using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class EnemyKill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void DieEnemy(GameObject gamObject)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        Destroy(gamObject);
    }
}
