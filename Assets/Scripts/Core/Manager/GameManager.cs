using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // 게임에 참여하는 총 인원
    public Dictionary<int, UserInfo> _userInfoDic = new Dictionary<int, UserInfo>();
    
    public int personCount = 0;

    public void ResetUserInfo()
    {
        _userInfoDic.Clear();
    }
    
    public bool AddUserInfo(UserInfo info)
    {
        return _userInfoDic.TryAdd(info.index, info);
    }
}
