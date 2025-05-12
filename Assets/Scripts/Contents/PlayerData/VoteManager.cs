using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoteManager
{
    private Dictionary<string, int> _voteData = new Dictionary<string, int>();

    public void Initialized(List<string> playerNames)
    {
        _voteData.Clear();
        
        foreach (var playerName in playerNames)
        {
            _voteData?.Add(playerName, 0);
        }
    }

    public void AddVote(string playerName)
    {
        _voteData[playerName] += 1;
    }

    public void ClearVoteCount()
    {
        foreach (var key in _voteData.Keys.ToList())
            _voteData[key] = 0;
    }

    /// <summary>
    /// 가장 많은 투표 수의 유저 이름을 가져온다.
    /// </summary>
    /// <returns>
    /// 카운트가 1개 -> 동표 없음
    /// 카운트가 2개 이상 -> 동표 있음
    /// </returns>
    public List<string> GetMaxVotePlayerName()
    {
        return SortVotes();
    }

    private List<string> SortVotes()
    {
        if (_voteData.Count == 0)
            return null;
        
        int maxVote = _voteData.Values.Max();
        return _voteData.Where(kv => kv.Value == maxVote)
            .Select(kv => kv.Key)
            .ToList();
    }
}
