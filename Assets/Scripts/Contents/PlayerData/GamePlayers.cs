using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayers
{
    private readonly List<UserInfo> _allPlayers = new List<UserInfo>();
    private UserInfo _assassinPlayer;
    private UserInfo _jokerPlayer;
    private List<UserInfo> _nonePlayers = new List<UserInfo>();
    
    public bool GeneratePlayersData(List<string> userNames)
    {
        if (userNames == null) return false;

        foreach (var name in userNames)
            _allPlayers.Add(new UserInfo(name));

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

    public List<UserInfo> GetPlayerData()
    {
        return _allPlayers;
    }
}
