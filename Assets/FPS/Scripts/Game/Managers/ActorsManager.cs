using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Unity.FPS.Game
{
    public class ActorsManager : MonoBehaviour
    {
        PhotonView view ;
        public List<Actor> Actors { get; private set; }
        public GameObject Player { get; private set; }

        public void SetPlayer(GameObject player) 
        {
            // if(view.IsMine)
            Player = player;
        
        }
        void Awake()
        {

            Actors = new List<Actor>();
            
        }
        void Start()
        {
            view=GetComponent<PhotonView>();
        }
    }
}
