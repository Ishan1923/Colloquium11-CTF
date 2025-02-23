using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GrabCredentials : MonoBehaviourPun
{

    [SerializeField] Text PlayerNameDisplay;
    PhotonView view;

    public string Local_player_name;
    public string Local_player_emailid;
    public string Local_player_rollno;
    public string lobby_id;
    public int Local_player_score;

    // Start is called before the first frame update
    void Start()
    {

        view = GetComponent<PhotonView>();

        if(view == null){
            return;
        }

        if(view.IsMine){
            PlayerNameDisplay.text = PhotonNetwork.LocalPlayer.NickName;
            Local_player_name = PlayerNameDisplay.text;

        }
        else{
            PlayerNameDisplay.text = photonView.Owner.NickName;
        }
    }

    void Update(){
        if(view != null){
            if(view.IsMine){
                if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerEmailId")){
                    Local_player_emailid = (string)PhotonNetwork.LocalPlayer.CustomProperties["PlayerEmailId"];
                }
                
                if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerRollNo")){
                 Local_player_rollno = (string)PhotonNetwork.LocalPlayer.CustomProperties["PlayerRollNo"];
                }
                
                if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerEmailScore_")){
                    object playerscore = PhotonNetwork.LocalPlayer.CustomProperties["PlayerScore_"];
                    if(playerscore == null)
                    Local_player_score = (int)playerscore;
                }
                lobby_id = PhotonNetwork.CurrentRoom.Name;
            }
        }
    }

}
