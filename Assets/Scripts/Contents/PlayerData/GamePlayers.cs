using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GamePlayers
{
    private readonly List<UserInfo> _allPlayers = new List<UserInfo>();
    private UserInfo _assassinPlayer;
    private UserInfo _jokerPlayer;
    private List<UserInfo> _hostages = new List<UserInfo>();
    private UserInfo _finalVoteTarget; // 최후 투표 지목자
    private List<string> _yesNoChoices = new List<string>(); // 예/아니오 선택 리스트

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

    public int GetPlayerCount()
    {
        return _allPlayers.Count;
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

    /// 현재 순서의 유저 데이터를 반환한다.
    public UserInfo GetCurrentPlayerData()
    {
        return _context.GetStrategy().GetCurrentPlayer();
    }

    /// 다음 순서의 유저 데이터를 반환한다.
    public UserInfo GetNextPlayerData()
    {
        return _context.GetStrategy().GetNextPlayer();
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

    public void UpdateNextPlayer()
    {
        _context.GetStrategy().UpdateNextPlayer();
    }

    public void CleanTurn()
    {
        foreach (var player in _allPlayers)
        {
            if (player == null) break;
            player.isOrder = false;
        }
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

    public void SetFinalVoteTarget(UserInfo targetPlayer)
    {
        _finalVoteTarget = targetPlayer;
    }

    public void SetFinalVoteTarget(string playerName)
    {
        var targetPlayer = FindPlayer(playerName);
        if (targetPlayer != null)
        {
            SetFinalVoteTarget(targetPlayer);
        }
    }

    public UserInfo GetFinalVoteTarget()
    {
        return _finalVoteTarget;
    }

    #region Yes/No Choices
    
    public void AddYesNoChoice(string choice)
    {
        if (choice == "예" || choice == "아니오")
        {
            _yesNoChoices.Add(choice);
        }
    }

    public void AddYesNoChoice(bool isYes)
    {
        string choice = isYes ? "예" : "아니오";
        _yesNoChoices.Add(choice);
    }

    public string GetMostChosenAnswer()
    {
        if (_yesNoChoices.Count == 0)
            return "예"; // 기본값

        int yesCount = _yesNoChoices.Count(x => x == "예");
        int noCount = _yesNoChoices.Count(x => x == "아니오");

        if (yesCount > noCount)
            return "예";
        else if (noCount > yesCount)
            return "아니오";
        else
            return "동점";
    }

    public void ClearYesNoChoices()
    {
        _yesNoChoices.Clear();
    }
    
    #endregion
}
