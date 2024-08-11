using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<UserInfo> _userList = new List<UserInfo>();
    // 현재 직업이 암살자인 유저를 찾아둔다.
    private UserInfo _assUser;
    public UserInfo _currentUser;
    
    // 현재 인질로 지정된 유저를 찾아둔다.
    private UserInfo _hostageUser;

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
}

