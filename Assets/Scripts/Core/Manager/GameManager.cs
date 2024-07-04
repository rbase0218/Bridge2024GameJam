using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 게임에 참여하는 총 인원
    public Dictionary<int, UserInfo> _userInfoList = new Dictionary<int, UserInfo>();
    
    [HideInInspector]
    public int personCount = 0;
    
    
    public void SetUserInfo(UserInfo info)
    {
        // UserInfoList가 MaxCount보다 많을 경우 Return
    }
}
