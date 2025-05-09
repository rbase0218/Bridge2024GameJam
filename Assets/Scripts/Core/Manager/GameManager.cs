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

    public void AddHostage(UserInfo userInfo)
    {
        _gamePlayers.AddHostage(userInfo);
    }

    public void AddHostage(string playerName)
    {
        var playerData = FindPlayer(playerName); 
        _gamePlayers.AddHostage(playerData);
    }
    
    public bool IsLastPlayer()
    {
        return _gamePlayers.IsLastPlayer();
    }
    
    public UserInfo GetCurrentHostageName()
    {
        return _gamePlayers.GetCurrentHostage();
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