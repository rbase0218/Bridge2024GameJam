using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 참여 인원을 모두 관리한다.
    public List<UserInfo> _saveUserInfoList = new List<UserInfo>();
    
    // 사람 최대 수
    public int personMaxCount = 0;
    // 현재 Round의 인원 Index
    public int currPersonIndex = 0;
    // 현재 설정된 카테고리의 Index (큰 주제)
    public int currCategoryIndex = 0;
    // 현재 설정된 카테고리의 세부 주제 Index (진행중인 주제)
    public int currCategorySubIndex = 0;

    public int gameRound = 0;

    #region Game Settings
    public void ClearUser()
    {
        _saveUserInfoList.Clear();
    }
    
    public void AddUserInfo(UserInfo info)
    {
        _saveUserInfoList.Add(info);
    }

    public void SetGame(int count)
    {
        // Game Round == PersonMaxCount == Count
        gameRound = personMaxCount = count;
    }
    
    #endregion
    private void AddPerson(int num = 1)
    {
        // 합친 값이 더 크면, 0으로 만들고 return
        if (currPersonIndex + num > personMaxCount)
        {
            currPersonIndex = 0;
            return;
        }
        currPersonIndex += num;
    }
}
