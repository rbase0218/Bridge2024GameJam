using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class GameManager : MonoBehaviour
{
    public List<UserInfo> _userList = new List<UserInfo>();
    public List<UserInfo> _hostageList = new List<UserInfo>();
    
    public List<VoteData> _voteList = new List<VoteData>();

    // 현재 직업이 암살자인 유저를 찾아둔다.
    public UserInfo assUser;
    public UserInfo actorUser;
    public UserInfo currentUser;
    public UserInfo prevUser;
    // 현재 인질로 지정된 유저를 찾아둔다.
    public UserInfo hostageUser;
    public UserInfo voteUser;
    public UserInfo questionUser;

    public string selectUserName;

    public string questionText;

    public string gameTopic;

    public EJobType winnerJob;
    
    private void Awake()
    {
        // var user1 = new UserInfo("User1");
        // var user2 = new UserInfo("User2", EJobType.Assassin);
        // var user3 = new UserInfo("User3");
        // var user4 = new UserInfo("User4", EJobType.Actor);
        // var user5 = new UserInfo("User5");
        // var user6 = new UserInfo("User6");
        //
        // AddUser(user1, user2, user3, user4, user5, user6);
        // assUser = user4;
        // currentUser = user1;
        // //AddHostage(user3);
        //
        // gameTopic = "호랑이";
    }
    
    public void AddUser(params UserInfo[] users)
    {
        _userList.AddRange(users);
    }
    
    public bool NextUser()
    {
        var index = _userList.IndexOf(currentUser);
        if (index == _userList.Count - 1)
        {
            currentUser = _userList[0];
            return false;
        }

        currentUser = _userList[index + 1];
        return true;
    }
    
    public void AddHostage(UserInfo hostage)
    {
        if(hostage == null)
            return;
        
        if (_hostageList.Contains(hostage) == false)
        {
            Debug.Log("인질로 지정된 유저: " + hostage.userName);
            hostageUser = hostage;
            _hostageList.Add(hostage);
        }
        else
        {
            Debug.LogError("이미 인질로 지정된 유저입니다.");
        }
    }

    public void UndoHostage()
    {
        _hostageList.Remove(_hostageList[^1]);
    }

    public void AddVoteUser(UserInfo voteUser)
    {
        // Vote User가 존재하지 않거나
        if (voteUser == null)
            return;
        // Vote User가 이미 투표 된 유저라면 기존 수에 +1을 더해준다.
        foreach (var user in _voteList)
        {
            if (user.targetUser.userName == voteUser.userName)
            {
                user.voteCount++;
                return;
            }
        }
        
        // Vote User가 새로운 유저라면 새로운 VoteData를 생성한다.
        _voteList.Add(new VoteData(1, voteUser));
    }

    public UserInfo GetVoteUser()
    {
        return _voteList.OrderByDescending(x => x.voteCount).FirstOrDefault()?.targetUser;
    }

    public bool IsDupleVoteUser()
    {
        var voteCountList = _voteList.Select(x => x.voteCount).ToList();
        var sameVoteCount = voteCountList.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
        if (sameVoteCount.Count <= 0) 
            return false;

        int maxCount = sameVoteCount.Max();
        
        var voteMaxCount = voteCountList.Max();
        
        // 중복 수의 마지막 값과 voteList의 최대 값이 동일하다면 => 중복
        if (maxCount == voteMaxCount)
            return true;
        return false;
    }

    public UserInfo GetCurrentHostage()
    {
        return _hostageList[^1];
    }

    public void AddRangeUser(List<string> userNames)
    {
        _userList?.Clear();

        for (int i = 0; i < userNames.Count; ++i)
        {
            _userList?.Add(new UserInfo(userNames[i]));
        }

        // 참여 유저에게 직업 부여
        GiveUsersJob();
        currentUser = _userList[0];
    }

    public void PickGameTopic(int index)
    {
        var maxNum = Managers.Data.wordArray[index].Length;
        var randNum = Random.Range(0, maxNum);
        
        gameTopic = Managers.Data.wordArray[index][randNum];
        Debug.Log("게임 주제 : " + gameTopic);
    }
    
    public void WriteQuestion(string text) => questionText = text;
    
    public bool GetQuestionUser()
    {
        prevUser = currentUser;
        currentUser = RandomQuestionUser();
        
        if(currentUser == null)
            return false;
        else
        {
            currentUser.canQuestion = false;
            return true;
        }
    }
    
    public List<UserInfo> GetAnswerUserList()
    {
        var copyUserList = new List<UserInfo>(_userList);
        copyUserList.Remove(hostageUser);
        copyUserList.Remove(currentUser);
        copyUserList.RemoveAll(x=>x.isDie);
        return copyUserList;
    }

    public void SetQuestion(string inputText)
    {
        questionText = inputText;
    }
    
    public void SetCanAllQuestion()
    {
        foreach (var user in _userList)
        {
            user.canQuestion = true;
        }
    }

    public void SetAnswerUser(string text)
    {
        questionUser = _userList.Find(x => x.userName == text);
    }
}