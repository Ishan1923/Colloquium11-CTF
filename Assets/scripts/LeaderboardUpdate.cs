using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Unity.FPS.Gameplay;

[System.Serializable]
public class ScoreData {
    public string team_name;
    public int score;
}


public class LeaderboardUpdate : MonoBehaviour {
    private string updateUrl = "https://leaderboard-dot-nomadic-buffer-450805-u3.ue.r.appspot.com/api/update-score/";

    void Start()
    {
        updateUrl="https://leaderboard-dot-nomadic-buffer-450805-u3.ue.r.appspot.com/api/update-score/";
    }
    public void UpdateTeamScore(string teamName, int score) {
        StartCoroutine(PostScore(teamName, score));
    }

    IEnumerator PostScore(string teamName, int score) {
        ScoreData data = new ScoreData { team_name = teamName, score = score };
        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest www = new UnityWebRequest(updateUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error updating score: " + www.error);
        } else {
            Debug.Log("Score updated for team " + teamName);
        }
    }
}
