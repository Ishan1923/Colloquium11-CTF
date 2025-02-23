using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class GameManagerPlayerCredentials : MonoBehaviourPunCallbacks
{
    public string player_name;
    public string player_rollno;
    public string player_emailid;

    [SerializeField] GameObject canvas1;

    [SerializeField] GameObject canvas2;

    void Start(){
        canvas1.SetActive(true);
        canvas2.SetActive(false);
    }

    public void GrabPlayerName(string inputText){
        player_name = inputText;
    }

    public void GrabPlayerRollno(string inputText){
        player_rollno = inputText;
    }

    public void GrabPlayerEmaiID(string inputText){
        player_emailid = inputText;
    }

    public void Next(){
        if(!string.IsNullOrEmpty(player_name) || !string.IsNullOrEmpty(player_rollno) || !string.IsNullOrEmpty(player_emailid) || player_name.Length > 3 || player_rollno.Length == 9 || player_emailid.Length > 14){
            if(!string.IsNullOrWhiteSpace(player_name) || !string.IsNullOrWhiteSpace(player_rollno) || !string.IsNullOrWhiteSpace(player_emailid)){
                if(player_emailid.Substring((player_emailid.Length - 11), 11) == "@thapar.edu"){
                
                    PhotonNetwork.LocalPlayer.NickName = player_name;

                    Hashtable player_data = new Hashtable();

                    player_data["player roll no"] = player_rollno;
                    player_data["player email id"] = player_emailid;

                    canvas1.SetActive(false);
                    canvas2.SetActive(true);
                }
            }
            
        }
    }

}
