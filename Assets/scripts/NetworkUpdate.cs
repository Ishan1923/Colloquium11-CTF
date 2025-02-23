using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
// using UnityEditor.ShaderGraph.Serialization;
using UnityEngine.Networking;

// [System.Serializable]
// public class questions_answered{
//     public string questionid;
// }


[System.Serializable]
public class sessiondata{
    public string team_name;
    public int team_score;
    public List<string> questions_answered;
}

public class NetworkUpdate : MonoBehaviourPunCallbacks
{
    private string sessionurl = ""; //Your url to be entered here
    [SerializeField] GameObject playerEntryPrefab;
    [SerializeField] Transform PlayersDisplayContainer;

    private List<Player> playerslist = new List<Player>();


    void Start(){
        sessionurl = "";//Your url to be entered here
        playerslist = new List<Player>(PhotonNetwork.PlayerList);
        UpdatePlayerUI();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        playerslist.Add(newPlayer);
        UpdatePlayerUI();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        playerslist.Remove(otherPlayer);
        UpdatePlayerUI();
    }


    public void UpdatePlayerUI(){

        foreach(Transform child in PlayersDisplayContainer){
            Destroy(child.gameObject);
        }

        foreach(Player player in playerslist){
            if(player != null){
                GameObject playerEntry = Instantiate(playerEntryPrefab, PlayersDisplayContainer);
                TextMeshProUGUI text = playerEntry.GetComponent<TextMeshProUGUI>();

                RectTransform rectTransform = playerEntry.GetComponent<RectTransform>();

                int childCount = PlayersDisplayContainer.childCount;
                rectTransform.anchoredPosition = new Vector2(0, -10*(childCount - 1));

                string playername = player.NickName;
                int playerscore = 0;
                if (player.CustomProperties.ContainsKey("PlayerScore_")){
                    playerscore = (int)player.CustomProperties["PlayerScore_"];
                }
                text.text = $"{playername} - {playerscore}";
                text.fontSize = 12;
                text.color = Color.black;
                text.alignment = TextAlignmentOptions.Center;
                text.fontStyle = FontStyles.Italic | FontStyles.Bold;

                text.lineSpacing = 1.2f;

                text.enableWordWrapping = false;
                text.overflowMode = TextOverflowModes.ScrollRect;

                RectTransform parentTransform = PlayersDisplayContainer.GetComponent<RectTransform>();

                float parentWidth = parentTransform.rect.width; 

                rectTransform.sizeDelta = new Vector2(parentWidth-5, rectTransform.sizeDelta.y);
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if(changedProps.ContainsKey("PlayerScore_")){
            Debug.Log($"In NetworkUpdate: The {targetPlayer.NickName}'s has been updated.");
            UpdatePlayerUI();
        }
    }

    public void SendSessionData(List<string> questions_answered, int team_score, string team_name){
        // if(playerslist.Count == 1){
        Debug.Log("Send session called");
            StartCoroutine(PostSessionData(team_name, team_score, questions_answered));
        // }
    }

    IEnumerator PostSessionData(string team_nam, int team_scor, List<string> questions){
        Debug.Log("Ho gya post session data");
        sessiondata data = new sessiondata{team_name = team_nam, team_score = team_scor, questions_answered = questions};
        string jsondata = JsonUtility.ToJson(data);

        UnityWebRequest www = new UnityWebRequest(sessionurl, "POST");
        byte[] jsontosend = new System.Text.UTF8Encoding().GetBytes(jsondata);
        www.uploadHandler = new UploadHandlerRaw(jsontosend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success){
            Debug.LogError("Error updating score: " + www.error);
        }
        else{
            Debug.Log("Score updated for team " + team_nam);
        }
    }

}
