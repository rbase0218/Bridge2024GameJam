using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPlayer : IPlayerStrategy
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
    public UserInfo GetCurrentPlayer()
    {
        throw new System.NotImplementedException();
    }
    public UserInfo GetNextPlayer()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateNextPlayer()
    {
        UpdateNextQuestioner();
    }
    public void CheckCurrentPlayer()
    {
        throw new System.NotImplementedException();
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
        Debug.Log("다음 넥스트 인덱스 :" + _nextIndex);
    }

    private void RefreshCurrentPlayer()
    {
        if (_allPlayers[_currentIndex].isHostage || _allPlayers[_currentIndex].isDie)
        {
            UpdateNextQuestioner();
        }
    }
}