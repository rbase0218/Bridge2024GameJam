using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<UserInfo> _userList = new List<UserInfo>();
    public List<UserInfo> _hostageList = new List<UserInfo>();

    // 현재 직업이 암살자인 유저를 찾아둔다.
    private UserInfo _assUser;
    public UserInfo _currentUser;
    // 현재 인질로 지정된 유저를 찾아둔다.
    private UserInfo _hostageUser;
    public UserInfo _voteUser;

    private string questionText;
    
    private void Awake()
    {
        var user1 = new UserInfo("User1", EJobType.VIP);
        var user2 = new UserInfo("User2", EJobType.VIP);
        var user3 = new UserInfo("User3", EJobType.VIP);
        var user4 = new UserInfo("User4", EJobType.Assassin);
        
        AddUser(user1, user2, user3, user4);
        _assUser = user4;
        _currentUser = user1;
    }
    
    public void AddUser(params UserInfo[] users)
    {
        _userList.AddRange(users);
    }
    
    public bool NextUser()
    {
        var index = _userList.IndexOf(_currentUser);
        if (index == _userList.Count - 1)
            return false;

        _currentUser = _userList[index + 1];
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

    public void SetQuestion(string text)
    {
        questionText = text;
    }
}