using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotePicker : IPlayerStrategy
{
    private int _currentIndex;
    private int _nextIndex;
    
    private List<UserInfo> _allPlayers;
    
    public void Initialized(List<UserInfo> allPlayers)
    {
        _currentIndex = 0;
        _nextIndex = 1;
        
        _allPlayers = allPlayers;
    }
    
    public UserInfo GetCurrentPlayerData()
    {
        return _allPlayers[_currentIndex];
    }
    
    public UserInfo GetNextPlayerData()
    {
        return _allPlayers[_nextIndex];
    }
    
    public void UpdateNextPlayer()
    {
        UpdateNextVoter();
    }
    
    public bool IsLastPlayer()
    {
        return _currentIndex == _allPlayers.Count - 1;
    }

    private void UpdateNextVoter()
    {
        
    }
    
}