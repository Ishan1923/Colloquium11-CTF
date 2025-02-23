using UnityEngine;
using UnityEngine.UI;

public class PlayerCredentials : MonoBehaviour
{
    public string player_id;
    public string player_name;
    public int player_score;
    [SerializeField] Text Name;

    void Start(){
        player_score = 0;

        if(Name != null){
            Name.text = player_name;
        }
        else{
            Debug.Log("Not able to find!");
        }
        
    }
}
