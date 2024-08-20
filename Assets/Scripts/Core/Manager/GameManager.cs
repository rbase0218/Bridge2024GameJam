using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public List<UserInfo> _userList = new List<UserInfo>();
    public List<UserInfo> _hostageList = new List<UserInfo>();

    // 현재 직업이 암살자인 유저를 찾아둔다.
    public UserInfo assUser;
    public UserInfo currentUser;
    public UserInfo actorUser;
    // 현재 인질로 지정된 유저를 찾아둔다.
    private UserInfo hostageUser;
    public UserInfo voteUser;

    private string questionText;
    
    private void Awake()
    {
        var user1 = new UserInfo("User1", EJobType.Assassin);
        var user2 = new UserInfo("User2");
        var user3 = new UserInfo("User3");
        var user4 = new UserInfo("User4");
        
        AddUser(user1, user2, user3, user4);
        assUser = user4;
        currentUser = user1;
    }
    
    public void AddUser(params UserInfo[] users)
    {
        _userList.AddRange(users);
    }
    
    public bool NextUser()
    {
        var index = _userList.IndexOf(currentUser);
        if (index == _userList.Count - 1)
            return false;

        currentUser = _userList[index + 1];
        return true;
    }
    
    public void AddHostage(UserInfo hostage)
    {
        if(_hostageList.Contains(hostage) == false)
            _hostageList.Add(hostage);
        else
        {
            Debug.LogError("이미 인질로 지정된 유저입니다.");
        }
    }

    public void AddRangeUser(List<string> userNames)
    {
        _userList?.Clear();

        for (int i = 0; i < userNames.Count; ++i)
        {
            _userList?.Add(new UserInfo(userNames[i]));
        }

        GiveUsersJob();
        currentUser = _userList[0];
    }
}