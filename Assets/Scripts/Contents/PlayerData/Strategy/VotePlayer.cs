using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotePlayer : IPlayerStrategy
{
    private int _currentIndex;
    private int _nextIndex;

    private bool _isLastEmpty;
    
    private List<UserInfo> _allPlayers;
    
    public void Initialized(List<UserInfo> allPlayers)
    {
        _currentIndex = 0;
        _nextIndex = 1;
        
        _allPlayers = allPlayers;
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
        UpdateNextVoter();
    }
    public bool ValidateCurrentAndNextPlayers()
    {
        int loopCount = 0;
        
        while (true)
        {
            var currentPlayer = GetCurrentPlayer();
            if (!(currentPlayer.isDie || currentPlayer.isOrder))
                break;

            UpdateNextPlayer();
            loopCount++;
            if (_isLastEmpty || loopCount > _allPlayers.Count)
                return false;
        }

        loopCount = 0;
        
        var nextPlayer = GetNextPlayer();
        while (nextPlayer.isDie || nextPlayer.isOrder)
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

    private void UpdateNextVoter()
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
        
        // 투표 권한이 있는 유저.
        // 1. 인질이 아닌 상태
        while ( _allPlayers[nextIndex].isHostage ||
                _allPlayers[nextIndex].isDie ||
                _allPlayers[nextIndex].isOrder)
        {
            nextIndex++;
            
            if (nextIndex >= _allPlayers.Count)
            {
                _isLastEmpty = true;
                return;
            }
        }
        
        _nextIndex = nextIndex;
    }
}