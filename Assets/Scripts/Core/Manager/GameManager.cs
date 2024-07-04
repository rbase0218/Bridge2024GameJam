using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // 게임에 참여하는 총 인원
    public Dictionary<int, UserInfo> _userInfoDic = new Dictionary<int, UserInfo>();
    
    public int personCount = 0;

    public void Reset()
    {
        _userInfoDic.Clear();
    }
    
    public void AddUserInfo(UserInfo info)
    {
        _userInfoDic.Add(info.index, info);
    }
}
