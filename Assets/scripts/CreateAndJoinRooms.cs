using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine.SocialPlatforms.Impl;
using Photon.Realtime;
using System.Collections.Generic;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public string createInput;
    public string joinInput;

    string PlayerRollNumber;
    string PlayerEmailID;
    int Score;
    string LobbyID;
    public GameObject loading;

    [SerializeField] GameObject GameManagerCred;
    
    void Start(){
        PlayerEmailID = "--";
        PlayerRollNumber = "--";
        Score = 0;
        LobbyID = "--";
    }

    public void CreateRoom(){
        if(GameManagerCred == null){
            return;
        }
        if(!string.IsNullOrEmpty(createInput) || !string.IsNullOrWhiteSpace(createInput))
        {

            RoomOptions room_options = new RoomOptions();
            room_options.MaxPlayers = 4;

            Hashtable roomProp = new Hashtable{
                {"RoomScore_", 0}
            };

            room_options.CustomRoomProperties = roomProp;
            room_options.CustomRoomPropertiesForLobby = new string[] { "RoomScore_" };

            // Debug.Log($"In CreateRoom: LobbyID = {LobbyID}");
            // Debug.Log($"In CreateRoom: PlayerEmailIDID = {PlayerEmailID}");
            // Debug.Log($"In CreateRoom: EncryptedRollNo = {PlayerRollNumber}");
            loading.SetActive(true);
            LobbyID = PhotonNetwork.CurrentLobby.Name;
            PhotonNetwork.LocalPlayer.NickName = GameManagerCred.GetComponent<GameManagerPlayerCredentials>().player_name;
            PhotonNetwork.CreateRoom(createInput,room_options);
        }
    }

    public override void OnCreatedRoom(){
        Debug.Log($"Room Created: {PhotonNetwork.CurrentRoom.Name}");
        SetRoomProperties();
    }

    public void JoinRoom(){
        if(GameManagerCred == null){
            Debug.LogWarning("In CreateAndJoinRooms Script GameManagerCred is null.");
            return;
        }
        if(!string.IsNullOrEmpty(joinInput) || !string.IsNullOrWhiteSpace(joinInput))
        {
            // Debug.Log($"In JoinRoom: LobbyID = {LobbyID}");
            // Debug.Log($"In JoinRoom: PlayerEmailIDID = {PlayerEmailID}");
            // Debug.Log($"In JoinRoom: EncryptedRollNo = {PlayerRollNumber}");
            loading.SetActive(true);
            LobbyID = PhotonNetwork.CurrentLobby.Name;
            PhotonNetwork.LocalPlayer.NickName = GameManagerCred.GetComponent<GameManagerPlayerCredentials>().player_name;
            PhotonNetwork.JoinRoom(joinInput);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        SetPlayerProperties(LobbyID);

        PhotonNetwork.LoadLevel("Main");

        // if(DataReached){
        //     PhotonNetwork.LoadLevel("Main");
        // }
        // else{
        //     Debug.LogWarning("Error setting player data on server.");
        // }
        
    }

    private void SetPlayerProperties(string lobby_id)
    {

        // Debug.Log($"In SetPlayerProperties: LobbyID = {lobby_id}");
        // Debug.Log($"In SetPlayerProperties: PlayerEmailIDID = {PlayerEmailID}");
        // Debug.Log($"In SetPlayerProperties: PlayerRollno = {PlayerRollNumber}");

        Hashtable LocalPlayerProperties = new Hashtable();
        LocalPlayerProperties["PlayerEmailId"] = PlayerEmailID;
        LocalPlayerProperties["PlayerRollNo"] = PlayerRollNumber;
        LocalPlayerProperties["PlayerLobbyID"] = lobby_id;
        LocalPlayerProperties["PlayerScore_"] = Score;
        PhotonNetwork.LocalPlayer.SetCustomProperties(LocalPlayerProperties);

        Debug.Log("Properties set locally: " + PhotonNetwork.LocalPlayer.CustomProperties.ToStringFull());
    }


    private void SetRoomProperties(){
        Hashtable RoomProp = new Hashtable();
        RoomProp["RoomScore_"] = Score;
        PhotonNetwork.CurrentRoom.SetCustomProperties(RoomProp);
    
        Debug.Log("Properties set to room: " + PhotonNetwork.CurrentRoom.CustomProperties.ToStringFull());
    
    }

    // private void GetPlayerPropertiesFromServer(){
    //     Hashtable LocalPlayerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
    //     Debug.Log("From Server: ");
    //     if (LocalPlayerProperties.ContainsKey("PlayerEmailId"))
    //     {
    //         string playerEmailId = (string)LocalPlayerProperties["PlayerEmailId"];
    //         Debug.Log("PlayerEmailId: " + playerEmailId);
    //     }

    //     if (LocalPlayerProperties.ContainsKey("PlayerRollNo"))
    //     {
    //         string playerRollNo = (string)LocalPlayerProperties["PlayerRollNo"];
    //         Debug.Log("PlayerRollNo: " + playerRollNo);
    //     }

    //     if (LocalPlayerProperties.ContainsKey("PlayerLobbyID"))
    //     {
    //         string playerLobbyId = (string)LocalPlayerProperties["PlayerLobbyID"];
    //         Debug.Log("PlayerLobbyID: " + playerLobbyId);
    //     }

    //     // if(!LocalPlayerProperties.ContainsKey("PlayerEmailId") || !LocalPlayerProperties.ContainsKey("PlayerRollNo") || !LocalPlayerProperties.ContainsKey("PlayerLobbyID")){
    //     //     return false;
    //     // }

    //     // return true;
    // }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        Debug.Log("Properties updated on the server: " + changedProps.ToStringFull());
    }


    // private IEnumerator SendDataToServer(string lobby_id){
    //     Debug.Log("Sending Data to server");
    //     SetPlayerProperties(lobby_id);
    //     yield return new WaitForSeconds(2f);
    //     Debug.Log("Recieved Data:");
    //     GetPlayerPropertiesFromServer();
    // }

    public void GrabCreatedCode(string inputText){
        createInput = inputText;
        LobbyID = inputText;
        GetDataFromGameManager();
    }

    public void GrabJoinCode(string inputText){
        joinInput = inputText;
        LobbyID = inputText;
        GetDataFromGameManager();
    }

    void GetDataFromGameManager(){
        PlayerEmailID = GameManagerCred.GetComponent<GameManagerPlayerCredentials>().player_emailid;
        PlayerRollNumber = GameManagerCred.GetComponent<GameManagerPlayerCredentials>().player_rollno;
    }
    
}
