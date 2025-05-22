using System.Collections;
using System.Collections.Generic;

public class GamePlayers
{
    private readonly List<UserInfo> _allPlayers = new List<UserInfo>();
    private UserInfo _assassinPlayer;
    private UserInfo _jokerPlayer;
    private List<UserInfo> _hostages = new List<UserInfo>();

    private PlayersDataContext _context = new PlayersDataContext();
    private VoteManager _voteManager = new VoteManager();

    public bool GeneratePlayersData(List<string> userNames)
    {
        if (userNames == null) return false;

        foreach (var name in userNames)
            _allPlayers.Add(new UserInfo(name));
        
        _context.Initialized(_allPlayers);
        _voteManager.Initialized(userNames);
        
        return true;
    }

    public void SetContext(PlayersDataContext.DataContextType type)
    {
        _context.SetupPlayerStrategy(type);
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
        return _context.GetStrategy().GetCurrentPlayerData();
    }

    /// <summary>
    /// 다음 순서의 유저 데이터를 반환한다.
    /// </summary>
    /// <returns></returns>
    public UserInfo GetNextPlayerData()
    {
        return _context.GetStrategy().GetNextPlayerData();
    }

    public void AddHostage(UserInfo userInfo)
    {
        // 이미 인질로 붙잡힌 적이 없다면 인질 리스트에 추가한다.
        // 가장 마지막에 존재하는 유저가 인질로 판정하기 위함
        if (!IsPlayerAlreadyHostage(userInfo.userName))
        {
            userInfo.isHostage = true;
            _hostages?.Add(userInfo);
        }
    }

    public void UndoHostage()
    {
        var hostage = _hostages[^1];
        hostage.isHostage = false;
        
        _hostages.RemoveAt(_hostages.Count - 1);
    }

    public bool IsPlayerAlreadyHostage(string playerName)
    {
        return _hostages.Find((x) => x.userName == playerName) != null;
    }

    public UserInfo GetCurrentHostage()
    {
        return _hostages[^1];
    }

    public bool IsLastPlayer()
    {
        return _context.GetStrategy().IsLastPlayer();
    }

    public void UpdateQuestioner()
    {
        _context.GetStrategy().UpdateNextPlayer();
    }

    public void AddVote(UserInfo userInfo)
    {
        _voteManager.AddVote(userInfo.userName);
    }

    public List<string> GetMaxVotePlayerName()
    {
        return _voteManager.GetMaxVotePlayerName();
    }

    public void ClearVoteCount()
    {
        _voteManager.ClearVoteCount();
    }

    public bool ValidateVictory()
    {
        return _hostages.Count == (_allPlayers.Count - 1);
    }
}
