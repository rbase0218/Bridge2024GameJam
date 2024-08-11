using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<UserInfo> _userList = new List<UserInfo>();

    // 현재 직업이 암살자인 유저를 찾아둔다.
    private UserInfo _assUser;
    
    // 현재 인질로 지정된 유저를 찾아둔다.
    private UserInfo _hostageUser;
    
    
    public void AddUser(params UserInfo[] users)
    {
        _userList.AddRange(users);
    }
}
