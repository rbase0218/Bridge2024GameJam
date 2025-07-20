using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPlayer : IPlayerStrategy
{
    private int _currentIndex;
    private int _nextIndex;

    private bool _isLastEmpty;
    
    private List<UserInfo> _allPlayers;

    public void Initialized(List<UserInfo> allPlayers)
    {
        Clean();
        
        _allPlayers = allPlayers;
    }

    public void Clean()
    {
        _currentIndex = 0;
        _nextIndex = 1;

        _isLastEmpty = false;
    }
    
    public UserInfo GetCurrentPlayer()
    {
        return _allPlayers[_currentIndex];
    }
    public UserInfo GetNextPlayer()
    {
        return _allPlayers[_nextIndex];
    }

    // 유저가 질문을 할 수 있는 조건
    // 1. 죽지 않은 상태여야 한다.
    // 2. 인질이 아닌 상태여야 한다.
    // 3. 자신의 차례가 이전에 실행되지 않았어야 한다.
    public bool ValidateCurrentAndNextPlayers()
    {
        int loopCount = 0;
        
        while (true)
        {
            var currentPlayer = GetCurrentPlayer();
            if (!(currentPlayer.isHostage || currentPlayer.isDie || currentPlayer.isOrder))
                break;

            UpdateNextPlayer();
            loopCount++;
            if (_isLastEmpty || loopCount > _allPlayers.Count)
                return false;
        }

        loopCount = 0;
        
        var nextPlayer = GetNextPlayer();
        while (nextPlayer.isHostage || nextPlayer.isDie || nextPlayer.isOrder)
        {
            _nextIndex++;
            loopCount++;

            if (_nextIndex >= _allPlayers.Count || loopCount > _allPlayers.Count)
            {
                _isLastEmpty = true;
                return false;
            }

            nextPlayer = GetNextPlayer();
        }

        return true;
    }

    public bool IsLastPlayer()
    {
        return _isLastEmpty;
    }

    public void UpdateNextPlayer()
    {
        var currentPlayer = GetCurrentPlayer();
        currentPlayer.isOrder = true;
        
        _currentIndex = _nextIndex;
        int nextIndex = _currentIndex + 1;

        if (nextIndex >= _allPlayers.Count)
        {
            _isLastEmpty = true;
            return;
        }

        // 현재 차례가 될 수 없거나
        // 현재 인질 상태이거나
        // 현재 죽은 상태라면
        // 다음 플레이어 중에서 조건에 부합하지 않은 플레이어를 찾는다.
        while ( _allPlayers[nextIndex].isOrder   ||
                _allPlayers[nextIndex].isHostage ||
                _allPlayers[nextIndex].isDie)
        {
            nextIndex++;
            
            if (nextIndex >= _allPlayers.Count)
            {
                _isLastEmpty = true;
                return;
            }
        }
        
        _nextIndex = nextIndex;
        Debug.Log("NEXT INDEX : " + _allPlayers[_nextIndex].userName);
    }
}