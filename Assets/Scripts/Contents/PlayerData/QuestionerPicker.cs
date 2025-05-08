using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionerPicker
{
    private int _currentQuestionerIndex;
    private int _nextQuestionerIndex;
    
    private List<UserInfo> _allPlayers;

    public void Initialized(List<UserInfo> allPlayers)
    {
        _currentQuestionerIndex = 0;
        _nextQuestionerIndex = 1;
        
        _allPlayers = allPlayers;
    }

    public int GetCurrentQuestionerIndex()
    {
        return _currentQuestionerIndex;
    }

    public int GetNextQuestionerIndex()
    {
        var savedIndex = _nextQuestionerIndex;
        UpdateNextQuestioner();
        return savedIndex;
    }

    public bool IsLastQuestioner()
    {
        return _currentQuestionerIndex == _allPlayers.Count - 1;
    }

    private void UpdateNextQuestioner()
    {
        _currentQuestionerIndex = _nextQuestionerIndex;
        int nextIndex = _currentQuestionerIndex + 1;
        
        // 질문을 할 수 있는 유저
        // 1. 현재 인질인 상태가 아니어야 한다.
        // 2. 현재 죽은 상태가 아니어야 한다.
        while (_allPlayers[nextIndex].isHostage || _allPlayers[nextIndex].isDie)
        {
            nextIndex++;
            
            if(nextIndex >= _allPlayers.Count)
                nextIndex = 0;
        }
        
        _nextQuestionerIndex = nextIndex;
    }
}