using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JobCheckStage에서 사용되는 Players 데이터

public class NormalPlayer : IPlayerStrategy
{
    private int _currentIndex;
    private int _nextIndex;

    private bool _isLastEmpty = false;
    
    private List<UserInfo> _allPlayers;

    public void Initialized(List<UserInfo> allPlayers)
    {
        _currentIndex = 0;
        _nextIndex = 1;

        _isLastEmpty = false;
        
        // 플레이어 데이터 등록
        _allPlayers = allPlayers;
    }

    public bool ValidateCurrentAndNextPlayers()
    {
        // 검증할 필요가 존재하지 않음.
        return true;
    }
    
    public bool IsLastPlayer()
    {
        return _isLastEmpty;
    }

    public UserInfo GetCurrentPlayer()
    {
        return _allPlayers[_currentIndex];
    }
    
    public UserInfo GetNextPlayer()
    {
        return _allPlayers[_nextIndex];
    }
    
    public void UpdateNextPlayer()
    {
        var currentPlayer = GetCurrentPlayer();
        currentPlayer.isOrder = true;
        
        // 현재 인덱스를 다음 인덱스로 변경한다.
        _currentIndex = _nextIndex;
        // 다음 인덱스를 변경한다.
        int nextIndex = _currentIndex + 1;

        
        // 다음 인덱스가 범위를 벗어난다면.
        if (nextIndex >= _allPlayers.Count)
        {
            _isLastEmpty = true;
            return;
        }
        
        // 다음 인덱스의 isOrder가 False면 While문에 접근하지 않는다.
        while (_allPlayers[nextIndex].isOrder)
        {
            nextIndex++;
        }
        
        // 검증을 거친 인덱스로 변경한다.
        _nextIndex = nextIndex;
    }
}
