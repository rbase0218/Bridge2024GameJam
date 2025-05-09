using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayers
{
    private QuestionerPicker _questionerPicker = new QuestionerPicker();
    
    private readonly List<UserInfo> _allPlayers = new List<UserInfo>();
    private UserInfo _assassinPlayer;
    private UserInfo _jokerPlayer;
    private List<UserInfo> _nonePlayers = new List<UserInfo>();
    
    private List<UserInfo> _hostages = new List<UserInfo>();

    public bool GeneratePlayersData(List<string> userNames)
    {
        if (userNames == null) return false;

        foreach (var name in userNames)
            _allPlayers.Add(new UserInfo(name));
        
        _questionerPicker.Initialized(_allPlayers);
        return true;
    }

    public bool ClearAllPlayers()
    {
        if(_allPlayers == null) return false;

        _allPlayers.Clear();
        return true;
    }

    public bool AllocatePlayerJobs()
    {
        return JobRandomizer.SelectRandomJob(_allPlayers, _assassinPlayer, _jokerPlayer);
    }

    
    /// <summary>
    /// 지정된 사용자 이름으로 플레이어를 찾는다.
    /// </summary>
    /// <param name="playerName">찾을 플레이어의 사용자 이름</param>
    /// <returns>찾은 플레이어의 UserInfo 객체, 찾지 못한 경우 null을 반환</returns>
    public UserInfo FindPlayer(string playerName)
    {
        return _allPlayers.Find((x) => x.userName == playerName);
    }
    
    /// <summary>
    /// 모든 유저의 데이터를 반환한다.
    /// </summary>
    /// <returns></returns>
    public List<UserInfo> GetAllPlayerData(string unViewName = null)
    {
        if (unViewName != null)
            return _allPlayers.FindAll((x) => x.userName != unViewName);
        return _allPlayers;
    }

    /// <summary>
    /// 현재 순서의 유저 데이터를 반환한다.
    /// </summary>
    /// <returns></returns>
    public UserInfo GetCurrentPlayerData()
    {
        return _allPlayers[_questionerPicker.GetCurrentQuestionerIndex()];
    }

    /// <summary>
    /// 다음 순서의 유저 데이터를 반환한다.
    /// </summary>
    /// <returns></returns>
    public UserInfo GetNextPlayerData()
    {
        return _allPlayers[_questionerPicker.GetNextQuestionerIndex()];
    }

    public void AddHostage(UserInfo userInfo)
    {
        // 이미 인질로 붙잡힌 적이 없다면 인질 리스트에 추가한다.
        // 가장 마지막에 존재하는 유저가 인질로 판정하기 위함
        if(!IsPlayerAlreadyHostage(userInfo.userName))
            _hostages?.Add(userInfo);
    }

    public bool IsPlayerAlreadyHostage(string playerName)
    {
        return _hostages.Find((x) => x.userName == playerName) != null;
    }

    public UserInfo GetCurrentHostage()
    {
        Debug.Log("수 : " + _hostages.Count);
        foreach (var userInfo in _hostages)
        {
            Debug.Log(userInfo.userName);
        }
        
        return _hostages[^1];
    }

    public bool IsLastPlayer()
    {
        return _questionerPicker.IsLastQuestioner();
    }
}
