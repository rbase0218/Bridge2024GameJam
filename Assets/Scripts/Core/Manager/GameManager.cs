using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private GamePlayers _gamePlayers;
    private TopicPicker _topicPicker;
    private QuestionLogManager _questionLogManager;
    public bool isGameEnd;

    private EJobType _winType = 0;
    
    private void Start()
    {
        if (Managers.Game.isGameEnd)
        {
            Managers.Sound.SetBGMVolume(Managers.Data.BGMVolume / 0.25f);
        }
        else
        {
            Managers.Sound.SetBGMVolume(Managers.Data.BGMVolume);
        }
        Managers.Sound.StopSFX();
        Managers.Sound.PlayBGM("Title");
    }

    private void Initialized()
    {
        _gamePlayers = new GamePlayers();
        _topicPicker = new TopicPicker();
        _questionLogManager = new QuestionLogManager();

        _winType = 0;
        isGameEnd = false;
    }

    // 타이틀 Scene -> Play Scene
    public bool SetUpPlayers(List<string> playerNames)
    {
        // Nickname -> PlayerData 생성
        var onGenerate = _gamePlayers.GeneratePlayersData(playerNames);
        if (!onGenerate)
            Debug.Log("게임 플레이어 생성에 실패했습니다.");

        // 유저들에게 직업을 분배한다.
        var onRegisterJobs = _gamePlayers.AllocatePlayerJobs();
        
        return onRegisterJobs;
    }

    public void SetContext(PlayersDataContext.DataContextType type)
    {
        _gamePlayers.SetContext(type);
    }

    public bool ValidateVictory()
    {
        return _gamePlayers.ValidateVictory();
    }

    public void SetWinner(EJobType type)
    {
        _winType = type;
    }

    public EJobType GetWinner()
    {
        return _winType;
    }
    
    #region GamePlayers

    public UserInfo FindPlayer(string playerName)
    {
        return _gamePlayers.FindPlayer(playerName);
    }

    public List<UserInfo> GetAllPlayers(string unViewName = null)
    {
        return _gamePlayers.GetAllPlayerData(unViewName);
    }

    public UserInfo GetCurrentPlayer()
    {
        return _gamePlayers.GetCurrentPlayerData();
    }

    public UserInfo GetNextPlayer()
    {
        return _gamePlayers.GetNextPlayerData();
    }

    public void UpdateQuestioner()
    {
        _gamePlayers.UpdateQuestioner();
    }

    public void AddHostage(UserInfo userInfo)
    {
        _gamePlayers.AddHostage(userInfo);
    }

    public void AddHostage(string playerName)
    {
        var playerData = FindPlayer(playerName); 
        _gamePlayers.AddHostage(playerData);
    }

    public void UndoHostage()
    {
        _gamePlayers.UndoHostage();
    }
    public bool IsLastPlayer()
    {
        return _gamePlayers.IsLastPlayer();
    }
    
    public UserInfo GetCurrentHostageName()
    {
        return _gamePlayers.GetCurrentHostage();
    }

    public void AddVote(UserInfo userInfo)
    {
        _gamePlayers.AddVote(userInfo);
    }

    public List<string> GetMaxVotePlayerName()
    {
        return _gamePlayers.GetMaxVotePlayerName();
    }

    public void ClearVoteCount()
    {
        _gamePlayers.ClearVoteCount();
    }
    
    #endregion
    
    #region TopicPicker
    public void PickTopic(int index)
    {
        Initialized();
        
        _topicPicker.PickTopic(index);
    }

    public string GetCurrentTopic()
    {
        return _topicPicker.Topic;
    }
    #endregion
    
    #region QuestionLogManager

    public void CreateQuestionLog(QuestionLog questionLog)
    {
        _questionLogManager.AddQuestionLog(questionLog);
    }

    public QuestionLog GetLastQuestionLog()
    {
        return _questionLogManager.GetLastQuestionLog();
    }

    public void ModifyQuestionLog(string answerer = null, string answer = null)
    {
        _questionLogManager.ModifyQuestionLog(answerer, answer);
    }
    
    #endregion
}