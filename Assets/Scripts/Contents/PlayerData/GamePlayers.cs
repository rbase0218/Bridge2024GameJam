using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayers
{
    private List<UserInfo> _players = new List<UserInfo>();
    private UserInfo _assassinPlayer;
    private UserInfo _jokerPlayer;
    private List<UserInfo> _nonePlayers = new List<UserInfo>();

    public void AddPlayer(UserInfo userInfo)
    {
        _players?.Add(userInfo);
    }
}
