using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 게임에 참여하는 총 인원
    private List<UserInfo> _userInfoList = new List<UserInfo>();
    
    public void SetUserInfo(UserInfo info)
    {
        // UserInfoList가 MaxCount보다 많을 경우 Return
        
        // info 데이터에 추가한다.
        _userInfoList.Add(info);
    }
}
