using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SocialPlatforms.Impl;
using ExitGames.Client.Photon;
using Unity.VisualScripting;
using Unity.FPS.Gameplay;
using Unity.FPS;
using UnityEngine.EventSystems;
using Unity.FPS.UI;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
[System.Serializable]
public class sessiondatawrapper{
    public sessiondata[] items;
}

public class GameManager : MonoBehaviour
{
  private string url ;
      public GameObject NetworkObject;
    public GameObject pickups;
    public GameObject env;
    public GameObject enemies;
    [SerializeField] GameObject checkpoint_screen;
    [SerializeField] GameObject player;
    [SerializeField] Text question_text;
    [SerializeField] Text question_id;
    [SerializeField] GameObject cam;
    [SerializeField] private string input;
    [SerializeField] GameObject CorrectPopUpUI;
    [SerializeField] GameObject WrongPopUpUI;
    public GameObject hint;
    public GameObject hintButton;
    private NetworkUpdate net;
    public List<string> questions = new List<string>();
    public string team_name;
    public int teamScore;
    GameObject[] checkpoints;

    public GameObject CurrCheckPoint;
    private Answers ans;
    public int roomScore;
    public Text roomScoreText;

    private float PopUpTime = 3f;
    public Text hintText;
    public InGameMenuManager inGameMenuManager;

    private int ScoreIncrement = 100;
    // int currentScore=0;
    // public PhotonView view;
    // public GameObject obj;
    public float time;
    int newTeamScore;

    void Start()
    {  newTeamScore = 0; 
    //Your url to be entered here
       url = "" + UnityWebRequest.EscapeURL(PhotonNetwork.CurrentRoom.Name);
      time=0f;
        ScoreIncrement=100;
        // view = GetComponent<PhotonView>();
        // currentScore=0;
        net=FindAnyObjectByType<NetworkUpdate>();
        player = GameObject.FindWithTag("Player");
        ans = GetComponent<Answers>();
        CorrectPopUpUI.SetActive(false);
        // player.AddComponent<PhotonView>();
        inGameMenuManager=FindAnyObjectByType<InGameMenuManager>();
        foreach (GameObject playerr in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (playerr.GetComponent<PhotonView>().IsMine)
            {
                player = playerr;
                break;
            }
        }
        // StartCoroutine(Fetchsessiondata());
        // ChangedScore();

         StartCoroutine(TurnThemOn());
        


    }
    private IEnumerator TurnThemOn()
    {
    yield return new WaitForSeconds(2f); // Equivalent to 2s delay
    env.SetActive(true);
    pickups.SetActive(true);
    enemies.SetActive(true);
    
    StartCoroutine(Fetchsessiondata());
}

    void LateUpdate()
    {   
        time+=Time.deltaTime;
        if(time>4f)
        {
          time=0f;
          ChangedScore();
          
        }
        if(player.GetComponent<PlayerCharacterController>().reduced)
        {
          player.GetComponent<PlayerCharacterController>().reduced=false;
          int currentScore=0;
          int currentTeamScore=0;
          if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("PlayerScore_", out object ScoreValue)){
              currentScore = (int)ScoreValue;
              Debug.Log($"In Func: {currentScore}");
            }
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RoomScore_", out object TeamScoreValue)){
              currentTeamScore = (int)TeamScoreValue;
              Debug.Log($"In Func: {currentTeamScore}");
            }
            int newScore=currentScore-10;
            newTeamScore=currentTeamScore-10;
            ExitGames.Client.Photon.Hashtable score = new ExitGames.Client.Photon.Hashtable();
            score["PlayerScore_"] = newScore;

            ExitGames.Client.Photon.Hashtable RoomProp = new ExitGames.Client.Photon.Hashtable();
            RoomProp["RoomScore_"] = newTeamScore;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(score);
            PhotonNetwork.CurrentRoom.SetCustomProperties(RoomProp);
          NetworkObject.GetComponent<LeaderboardUpdate>().UpdateTeamScore(PhotonNetwork.CurrentRoom.Name, newTeamScore);
          ScoreRED();
          ChangedScore();
        }
    }
    IEnumerator<UnityEngine.Networking.UnityWebRequestAsyncOperation> 
    Fetchsessiondata(){
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
        foreach(string question in questions)
          Debug.Log($"questions : {question}");
        if(questions.Contains(questionid)){
          checkpoint.GetComponent<CheckDestroy>().DestroyIt();
          Debug.Log($"Destroyed checkpoint with id: {questionid}");
        }
        else{
          Debug.LogWarning($"Checkpoint GameObject is missing the question id or is not there {questionid}");
        }
      }
    }

    public void CheckPoint(GameObject checkPointObj){
        if(player==null)
        return;
        
        Cursor.lockState = CursorLockMode.None;
        CurrCheckPoint=checkPointObj;
            Cursor.visible = true;
            // view=checkPointObj.GetComponent<PhotonView>();
            hint.SetActive(false);
            hintButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
        player.GetComponent<CharacterController>().detectCollisions = false;
        question_text.text = checkPointObj.GetComponent<QuestionData>().question;
        question_id.text = checkPointObj.GetComponent<QuestionData>().id;
        // uIManager.DisableLocalPlayer();
        checkpoint_screen.SetActive(true);


        inGameMenuManager.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<PlayerCharacterController>().enabled = false;
            player.GetComponent<PlayerInputHandler>().enabled = false;
            // Unlock Cursor
            
        Debug.Log("Ho gya checkpoint" );
    
   }
   public void CloseCheckPointScreen(){
    // if(player==null)
    //     return;
    //     checkpoint_screen.SetActive(false);
        
    //     Cursor.lockState = CursorLockMode.Locked;
    //         Cursor.visible = false;
    // Debug.Log("Ho gya Cancel");
    //  checkpoint_screen.SetActive(false);
    //  inGameMenuManager.enabled = true;
    //  player.GetComponent<CharacterController>().enabled = true;
    //  player.transform.position += new Vector3(1.5f, 0,0);
    //     player.GetComponent<PlayerCharacterController>().enabled = true; 
            
    //         player.GetComponent<PlayerInputHandler>().enabled = true;
    //         player.GetComponent<CharacterController>().detectCollisions = true;
        // uIManager.EnableLocalPlayer();
        // // player.GetComponent<ThirdPersonController>().enabled = true;
        // player.GetComponent<CharacterController>().detectCollisions = true;
        

   }

   public void ReadDataOnSubmit(string inputText){
        // Debug.Log($"{question_id.text} : {inputText}");

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
            foreach(string q in questions )
            Debug.Log($@"#####{q}");
            net.SendSessionData(questions, newTeamScore, PhotonNetwork.CurrentRoom.Name);
            ChangedScore();

          }
          else{
            WrongPopUpUI.SetActive(true);
            Invoke(nameof(SetWrongPopUpUIFalse), PopUpTime);
          }
            // new WaitForSeconds(5);
          CancelKar();
          
        // }

   }
    public void ScoreRED()
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

            int newScore = currentScore - 30;
             newTeamScore = currentTeamScore - 30;
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
            ScoreRED();
        ChangedScore();

   }
   private void SetCorrectPopUpUIFalse(){
    CorrectPopUpUI.SetActive(false);
  }
  private void SetWrongPopUpUIFalse(){
    WrongPopUpUI.SetActive(false);
  }

   public void CancelKar()
   {
    if(player==null)
        return;
    Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    Debug.Log("Ho gya Cancel");
     checkpoint_screen.SetActive(false);
    //  uIManager.EnableLocalPlayer();
    // //  player.GetComponent<ThirdPersonController>().enabled = true;
     
            Debug.Log("Done bhai");
            // Lock Cursor for FPS
            inGameMenuManager.enabled = true;
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<PlayerInputHandler>().enabled = true;
            player.transform.position += new Vector3(1.5f, 0,0);
            player.GetComponent<PlayerCharacterController>().enabled = true;
            player.GetComponent<CharacterController>().detectCollisions = true;
            // ChangedScore();
     
   }
  [PunRPC] public void DestroyCheckPoint(GameObject currCheck){
            // Master Client destroys the object across the network
            // if(
        PhotonNetwork.Destroy(currCheck);
          Debug.Log("Checkpoint destroyed!");
            // }
      //  CurrCheckPoint.SetActive(false);
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
