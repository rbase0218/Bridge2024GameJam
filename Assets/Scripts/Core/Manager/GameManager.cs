using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    private List<UserInfo> _userList = new List<UserInfo>();

    public UserInfo assUser;
    public UserInfo actorUser;
    public UserInfo hostageUser;

    public UserInfo currentUser;
    
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

