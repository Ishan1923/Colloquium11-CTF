using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SocialPlatforms.Impl;
using ExitGames.Client.Photon;
using Unity.VisualScripting;
using System.Collections;
using TMPro;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;


[System.Serializable]
 class sessiondatawrappers{
    public sessiondata[] items;
}

public class GameManager2 : MonoBehaviourPun
{
  private string url;
    public TMP_Text roomScoreText;
    public GameObject NetworkObject;
    [SerializeField] GameObject checkpoint_screen;
    [SerializeField] GameObject player;
    [SerializeField] Text question_text;
    [SerializeField] Text question_id;
    [SerializeField] GameObject cam;
    [SerializeField] private string input;
    [SerializeField] GameObject CorrectPopUpUI;
    [SerializeField] GameObject WrongPopUpUI;

    public GameObject CurrCheckPoint;
    private Answers2 ans;
    public PhotonView view;

    private float PopUpTime = 3f;

    private int ScoreIncrement = 10;
    public float time;
    public GameObject hint,hintButton;
    public int teamScore;
    GameObject[] checkpoints;
    
    public Text hintText;
    private NetworkUpdate net;
    public List<string> questions = new List<string>();
    public string team_name;
    int newTeamScore;
  void Start(){
    url = "" + UnityWebRequest.EscapeURL(PhotonNetwork.CurrentRoom.Name); //Your url to be entered here
    time=0f;
    newTeamScore=0;
        ScoreIncrement=200;
    player = GameObject.FindWithTag("Player");
        ans = GetComponent<Answers2>();
        CorrectPopUpUI.SetActive(false);
        net = FindAnyObjectByType<NetworkUpdate>();
        // player.AddComponent<PhotonView>();
        // inGameMenuManager=FindAnyObjectByType<InGameMenuManager>();
        foreach (GameObject playerr in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (playerr.GetComponent<PhotonView>().IsMine)
            {
                player = playerr;
                break;
            }
        }
        StartCoroutine(Fetchsessiondatas());
  }
  void LateUpdate()
    {   
        time+=Time.deltaTime;
        if(time>4f)
        {
          time=0f;
          ChangedScore();
          
        }
    }
    IEnumerator<UnityEngine.Networking.UnityWebRequestAsyncOperation> 
    Fetchsessiondatas(){
      using (UnityWebRequest request = UnityWebRequest.Get(url)){
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success){
          Debug.LogError("Error Fetching team data: " + request.error);
          teamScore = 0;
          
          Debug.Log("Team name not found");
          yield break;
        }
        else{
          sessiondatawrapper datawrapper = JsonUtility.FromJson<sessiondatawrapper>(request.downloadHandler.text);
        Debug.Log($"###########{request.downloadHandler.text}");
        sessiondata data = datawrapper.items[0];
        if(data != null && !string.IsNullOrEmpty(data.team_name)){
          team_name = data.team_name;
          teamScore = data.team_score;
          questions = data.questions_answered;
          Debug.Log($"Team {team_name} exists. Score : {teamScore}, Questions Answered: {questions}");

          ExitGames.Client.Photon.Hashtable roomScoreProp = new ExitGames.Client.Photon.Hashtable();
          roomScoreProp["RoomScore_"] = teamScore;
          PhotonNetwork.CurrentRoom.SetCustomProperties(roomScoreProp);

          DeleteQuestionsLocally();
        }
      }
    }
    }
    private void DeleteQuestionsLocally(){
      checkpoints = GameObject.FindGameObjectsWithTag("checkpoint");
      foreach(GameObject checkpoint in checkpoints){
        string questionid = checkpoint.GetComponent<QuestionData>().id;

        if(questions.Contains(questionid)){
          checkpoint.GetComponent<CheckDestroy>().DestroyIt();
          Debug.Log($"Destroyed checkpoint with id: {questionid}");
        }
        else{
          Debug.LogWarning("Checkpoint GameObject is missing the question id or is not there");
        }
      }
    }

   public void CheckPoint(GameObject checkPointObj){

          checkpoint_screen.SetActive(true);
        question_text.text = checkPointObj.GetComponent<QuestionData>().question;
        question_id.text = checkPointObj.GetComponent<QuestionData>().id;
        Cursor.lockState = CursorLockMode.None;
        CurrCheckPoint=checkPointObj;
        
        
        player.GetComponent<RunnerMovement>().enabled = false;
        player.GetComponent<CharacterController>().detectCollisions = false;
    
   }

   public void CloseCheckPointScreen(){
        checkpoint_screen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<RunnerMovement>().enabled = true;
        player.GetComponent<CharacterController>().detectCollisions = true;

   }

   public void ReadDataOnSubmit(string inputText){
        if(ans == null){
          return;
        }
        if(player==null)
        return;

        // if(player.GetComponent<PhotonView>().IsMine){
         if((string)ans.questions[question_id.text] == inputText){

          questions.Add(question_id.text);
            Debug.Log($"questions : {questions}");

            // Debug.Log($"Right answer, {(string)ans.questions[question_id.text]} and answerd is {inputText}");
            CorrectPopUpUI.SetActive(true);
            Invoke(nameof(SetCorrectPopUpUIFalse), PopUpTime);
            // // DestroyCheckPoint();
            // view.RPC("DestroyCheckPoint",RpcTarget.MasterClient,CurrCheckPoint);
            CurrCheckPoint.GetComponent<CheckDestroy>().DestroyIt();
            int currentScore = 0;
            int currentTeamScore=0;
            if(!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerScore_")){
              Debug.Log("PlayerScore_ DNE");
            }
            if(!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("PlayerScore_")){
              Debug.Log("RoomScore_ DNE");
            }
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("PlayerScore_", out object ScoreValue)){
              currentScore = (int)ScoreValue;
              Debug.Log($"In Func: {currentScore}");
            }
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RoomScore_", out object TeamScoreValue)){
              currentTeamScore = (int)TeamScoreValue;
              Debug.Log($"In Func: {currentTeamScore}");
            }
            Debug.Log($"out func: {currentScore}");

            int newScore = currentScore + ScoreIncrement;
             newTeamScore=currentTeamScore + ScoreIncrement;

            Debug.Log($"Before Setting up score: {newScore}");

            ExitGames.Client.Photon.Hashtable score = new ExitGames.Client.Photon.Hashtable();
            score["PlayerScore_"] = newScore;

            ExitGames.Client.Photon.Hashtable RoomProp = new ExitGames.Client.Photon.Hashtable();
            RoomProp["RoomScore_"] = newTeamScore;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(score);
            PhotonNetwork.CurrentRoom.SetCustomProperties(RoomProp);

            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("PlayerScore_", out object updatedScore)){
              Debug.Log($"The Player's updated Score = {updatedScore}");
            }
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RoomScore_", out object RoomUpdatedScore)){
              Debug.Log($"The Room's updated Score = {RoomUpdatedScore}");
            }


            NetworkObject.GetComponent<LeaderboardUpdate>().UpdateTeamScore(PhotonNetwork.CurrentRoom.Name, newTeamScore);
            // net.SendSessionData(questions, newTeamScore, PhotonNetwork.CurrentRoom.Name);
            ScoreREDD();
            ChangedScore();
          }
          else{
            WrongPopUpUI.SetActive(true);
            Invoke(nameof(SetWrongPopUpUIFalse), PopUpTime);
          }

          CloseCheckPointScreen();
        // }

   }
   public void ScoreREDD()
    {
      net.SendSessionData(questions, newTeamScore, PhotonNetwork.CurrentRoom.Name);
    }
   public void Hint()
   {
    if(player==null)
        return;
        
        int currentScore = 0;
        int currentTeamScore=0;
            if(!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerScore_")){
              Debug.Log("PlayerScore_ DNE");
            }
            if(!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("PlayerScore_")){
              Debug.Log("RoomScore_ DNE");
            }
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("PlayerScore_", out object ScoreValue)){
              currentScore = (int)ScoreValue;
              Debug.Log($"In Func: {currentTeamScore}");
            }
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RoomScore_", out object TeamScoreValue)){
              currentTeamScore = (int)TeamScoreValue;
              Debug.Log($"In Func: {currentScore}");
            }

            Debug.Log($"out func: {currentScore}");

            int newScore = currentScore - 50;
             newTeamScore = currentTeamScore - 50;
            hint.SetActive(true);
            hintText.text = (string)ans.hints[question_id.text] ;
            hintButton.SetActive(false);

            Debug.Log($"Before Setting up score: {newScore}");

            ExitGames.Client.Photon.Hashtable score = new ExitGames.Client.Photon.Hashtable();
            score["PlayerScore_"] = newScore;

            ExitGames.Client.Photon.Hashtable RoomProp = new ExitGames.Client.Photon.Hashtable();
            RoomProp["RoomScore_"] = newTeamScore;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(score);
            PhotonNetwork.CurrentRoom.SetCustomProperties(RoomProp);

            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("PlayerScore_", out object updatedScore)){
              Debug.Log($"The Player's updated Score = {updatedScore}");
            }
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RoomScore_", out object RoomUpdatedScore)){
              Debug.Log($"The Room's updated Score = {RoomUpdatedScore}");
            }
            NetworkObject.GetComponent<LeaderboardUpdate>().UpdateTeamScore(PhotonNetwork.CurrentRoom.Name, newTeamScore);
            ScoreREDD();
        ChangedScore();
   }
  private void SetCorrectPopUpUIFalse(){
    CorrectPopUpUI.SetActive(false);
  }

  private void SetWrongPopUpUIFalse(){
    WrongPopUpUI.SetActive(false);
  }

   public void Cancel(){
     checkpoint_screen.SetActive(false);
      Cursor.lockState = CursorLockMode.Locked;
     player.GetComponent<RunnerMovement>().enabled = true;
     player.GetComponent<CharacterController>().detectCollisions = true;
   }

   public void DestroyCheckPoint(GameObject checkPointObj){


        checkPointObj.GetComponent<CheckDestroy>().DestroyIt();
    
   }
       public void ChangedScore()
{
    StartCoroutine(ChangedScoreCoroutine());
}

private IEnumerator ChangedScoreCoroutine()
{
    yield return new WaitForSeconds(0.7f); // Equivalent to 500ms delay

    if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RoomScore_", out object RoomUpdatedScore))
    {
        Debug.Log($"The Room's updated Score = {RoomUpdatedScore}");
        roomScoreText.text = "Room Score = " + RoomUpdatedScore.ToString();
        
    }
}



}
