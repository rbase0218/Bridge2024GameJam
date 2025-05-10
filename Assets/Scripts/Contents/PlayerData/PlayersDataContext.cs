using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersDataContext
{
    public enum DataContextType
    {
        Questioner,
        Voter
    }
    
    private IPlayerStrategy _playerStrategy;
    private QuestionerPicker _questionerPicker = new QuestionerPicker();
    private VotePicker _votePicker = new VotePicker();

    public IPlayerStrategy GetStrategy()
    {
        return _playerStrategy;
    }

    public void Initialize(List<UserInfo> players)
    {
        _questionerPicker.Initialized(players);
        _votePicker.Initialized(players);
    }

    public void SetupPlayerStrategy(DataContextType type)
    {
        switch (type)
        {
            case DataContextType.Questioner:
                _playerStrategy = _questionerPicker;
                break;
            
            case DataContextType.Voter:
                _playerStrategy = _votePicker;
                break;
            
            default:
                break;
        }
    }
}
