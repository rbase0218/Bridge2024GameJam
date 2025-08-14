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

    public QuestionLogManager QuestionManager { get; private set; }
    public bool isGameEnd;
    public int CurrentRound { get; private set; } = 1; // 현재 라운드

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

        // 디버그를 위한 데이터 - 지우지마세욧.
        // var debugData = new List<string>();
        // debugData.Add("1");
        // debugData.Add("2");
        // debugData.Add("3");
        // debugData.Add("4");
        // PickTopic(0);
        // SetUpPlayers(debugData);
    }

    private void Initialized()
    {
        _gamePlayers = new GamePlayers();
        _topicPicker = new TopicPicker();
        QuestionManager = new QuestionLogManager();

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
        CleanTurn();
        _gamePlayers.SetContext(type);
    }

    public int GetPlayerCount()
    {
        return _gamePlayers.GetPlayerCount();
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

    public void UpdateNextPlayer()
    {
        _gamePlayers.UpdateNextPlayer();
    }

    public void CleanTurn()
    {
        _gamePlayers.CleanTurn();
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

    public UserInfo GetCurrentHostage()
    {
        return _gamePlayers.GetCurrentHostage();
    }

    public UserInfo GetLastHostage()
    {
        return _gamePlayers.GetLastHostage();
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

    #region Final Vote Target

    public void SetFinalVoteTarget(UserInfo targetPlayer)
    {
        _gamePlayers.SetFinalVoteTarget(targetPlayer);
    }

    public void SetFinalVoteTarget(string playerName)
    {
        _gamePlayers.SetFinalVoteTarget(playerName);
    }

    public UserInfo GetFinalVoteTarget()
    {
        return _gamePlayers.GetFinalVoteTarget();
    }

    #endregion

    #region Yes/No Choices
    
    public void AddYesNoChoice(string choice)
    {
        _gamePlayers.AddYesNoChoice(choice);
    }

    public void AddYesNoChoice(bool isYes)
    {
        _gamePlayers.AddYesNoChoice(isYes);
    }

    public string GetMostChosenAnswer()
    {
        return _gamePlayers.GetMostChosenAnswer();
    }

    public void ClearYesNoChoices()
    {
        _gamePlayers.ClearYesNoChoices();
    }
    
    #endregion

    #region Data Reset
    

    public void ResetAllGameData()
    {
        // 게임 상태 초기화
        isGameEnd = false;
        _winType = 0;
        CurrentRound = 1; // 라운드 초기화
        
        // 플레이어 데이터 초기화
        _gamePlayers.ClearAllPlayers();
        _gamePlayers.ClearYesNoChoices();
        _gamePlayers.ClearVoteCount();
        
        // 질문 로그 초기화
        if (QuestionManager != null)
        {
            QuestionManager.ClearQuestionLog();
        }
        
        // 최후 투표 지목자 초기화
        _gamePlayers.SetFinalVoteTarget((UserInfo)null);        
    }

    public void ResetPlayerData()
    {
        _gamePlayers.ClearAllPlayers();
        _gamePlayers.ClearVoteCount();
        _gamePlayers.SetFinalVoteTarget((UserInfo)null);        
    }

    public void ResetVoteData()
    {
        _gamePlayers.ClearVoteCount();
        _gamePlayers.SetFinalVoteTarget((UserInfo)null);        
    }

    public void ResetYesNoData()
    {
        _gamePlayers.ClearYesNoChoices();        
    }

    public void ResetQuestionData()
    {
        if (QuestionManager != null)
        {
            QuestionManager.ClearQuestionLog();
        }        
    }

    public void ResetGameState()
    {
        isGameEnd = false;
        _winType = 0;
    }
    
    #endregion

    public void PickRandomHostage()
    {
        // 살아있는 플레이어 리스트 추출
        var alivePlayers = GetAllPlayers().FindAll(player => !player.isDie);

        if (alivePlayers.Count == 0)
            return;

        // 랜덤 인덱스 선택
        int randomIndex = UnityEngine.Random.Range(0, alivePlayers.Count);
        var selected = alivePlayers[randomIndex];

        // 인질로 지정
        _gamePlayers.AddHostage(selected);
    }

    public void NextRound()
    {
        CurrentRound++;
    }
}