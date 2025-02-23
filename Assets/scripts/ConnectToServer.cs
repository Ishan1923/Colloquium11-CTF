using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "in";
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 60000;
        PhotonNetwork.KeepAliveInBackground = 60000;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to server");
    }

    public override void OnJoinedLobby()
    {
        // base.OnJoinedLobby();
        SceneManager.LoadScene("Entering");
    }


}
