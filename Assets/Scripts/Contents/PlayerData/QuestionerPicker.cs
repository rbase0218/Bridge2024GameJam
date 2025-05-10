using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionerPicker : IPlayerStrategy
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
        UpdateNextQuestioner();
    }

    public bool IsLastPlayer()
    {
        return _currentIndex == _allPlayers.Count - 1;
    }

    private void UpdateNextQuestioner()
    {
        _currentIndex = _nextIndex;
        int nextIndex = _currentIndex + 1;
        
        // 질문을 할 수 있는 유저
        // 1. 현재 인질인 상태가 아니어야 한다.
        // 2. 현재 죽은 상태가 아니어야 한다.
        if(nextIndex >= _allPlayers.Count)
            nextIndex = 0;
        
        while (_allPlayers[nextIndex].isHostage || _allPlayers[nextIndex].isDie)
        {
            nextIndex++;

            if (nextIndex >= _allPlayers.Count)
                break;
        }

        _nextIndex = nextIndex;
    }
}