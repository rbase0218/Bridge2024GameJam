using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private UIThis uiThis;
    [SerializeField] private UIWordCheck uiWordCheck;
    private List<UserInfo> userList;

    private int curUserCount;
    private int roundCount;
    
    private void Start()
    {
        userList = Managers.Game._saveUserInfoList;
        roundCount = Managers.Game.gameRound;
        curUserCount = 0;
        StartRound();
    }
    
    private void StartRound()
    {
        uiThis.OpenFrame(8);
        uiThis.SetFrame8(userList[0].name);
    }
    
    private void NextRound()
    {
        curUserCount++;
        if (curUserCount >= userList.Count)
        {
            Debug.Log("Round End");
            return;
        }
        
        uiThis.OpenFrame(8);
        uiThis.SetFrame8(userList[curUserCount].name);
    }
}
