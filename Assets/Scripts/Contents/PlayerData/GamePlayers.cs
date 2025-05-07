using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayers
{
    private List<UserInfo> _allPlayers = new List<UserInfo>();
    private UserInfo _assassinPlayer;
    private UserInfo _jokerPlayer;
    private List<UserInfo> _nonePlayers = new List<UserInfo>();

    public bool AddPlayer(UserInfo userInfo)
    {
        if (userInfo == null) return false;

        _allPlayers?.Add(userInfo);
        return true;
    }

    public bool ClearAllPlayers()
    {
        if(_allPlayers == null) return false;

        _allPlayers.Clear();
        return true;
    }

    public bool AllocatePlayerJobs()
    {
        return JobRandomizer.SelectRandomJob(_allPlayers, _assassinPlayer, _jokerPlayer);
    }
}
