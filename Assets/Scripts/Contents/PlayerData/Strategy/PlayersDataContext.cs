using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersDataContext
{
    public enum DataContextType
    {
        Normal,
        Questioner,
        Voter
    }
    
    private IPlayerStrategy _playerStrategy;
    
    private NormalPlayer _normalPlayer = new NormalPlayer();
    private QuestionPlayer _questionPlayer = new QuestionPlayer();
    private VotePlayer _votePlayer = new VotePlayer();

    public IPlayerStrategy GetStrategy()
    {
        return _playerStrategy;
    }

    public void Initialized(List<UserInfo> players)
    {
        _normalPlayer.Initialized(players);
        _questionPlayer.Initialized(players);
        _votePlayer.Initialized(players);
    }

    public void SetupPlayerStrategy(DataContextType type)
    {
        switch (type)
        {
            case DataContextType.Normal:
                _playerStrategy = _normalPlayer;
                break;
            
            case DataContextType.Questioner:
                _playerStrategy = _questionPlayer;
                break;
            
            case DataContextType.Voter:
                _playerStrategy = _votePlayer;
                break;
            
            default:
                break;
        }
        
        // 첫 순서로 지정된 유저가 유효한지 확인한다.
        _playerStrategy.CheckCurrentPlayer();
    }
}
