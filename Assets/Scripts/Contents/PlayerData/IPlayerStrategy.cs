using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStrategy
{
    public void Initialized(List<UserInfo> allPlayers);
    public UserInfo GetPlayerData(int index = 0);
    
    public UserInfo GetCurrentPlayerData();
    public UserInfo GetNextPlayerData();

    public void UpdateNextPlayer();
    
    public bool IsLastPlayer();
    
    public int GetPlayerCount();
}
