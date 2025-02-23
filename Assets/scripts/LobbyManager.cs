using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private List<TypedLobbyInfo> lobbyStatistics = new List<TypedLobbyInfo>();

    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> UpadatedLobbyStatistics)
    {
        base.OnLobbyStatisticsUpdate(UpadatedLobbyStatistics);

        lobbyStatistics = UpadatedLobbyStatistics;

        Debug.Log("Total Lobbies: " + lobbyStatistics.Count);

        foreach (var lobbyInfo in lobbyStatistics){
            Debug.Log("Lobby Name: " + lobbyInfo.Name + " , Type: " + lobbyInfo.Type + ", Number of Lobbies : " + lobbyInfo.RoomCount);
        }
    }

}