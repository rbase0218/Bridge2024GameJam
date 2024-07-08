using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public UIThis uiThis;
    public UIWordCheck uiWordCheck;
    private List<UserInfo> userList;

    private int curUserCount;
    private int roundCount;
    private string secretWord;
    private UserInfo hostageUser;
    
    private void Start()
    {
        var categoryWord = Managers.Data.categoryArray[Managers.Game.currCategoryIndex];
        var len = Managers.Data.categoryDic[categoryWord].Length;
        secretWord = Managers.Data.categoryDic[categoryWord][Random.Range(0, len)];
        userList = Managers.Game._saveUserInfoList;
        roundCount = Managers.Game.gameRound;
        curUserCount = 0;
        roundCount = 0;
        StartRound();
    }
    
    private void StartRound()
    {
        uiThis.gameObject.SetActive(true);
        uiThis.OpenFrame(8);
        uiThis.SetFrame8(userList[curUserCount].name);
        // 원래 인트로로 시작
    }

    public void SetCurHostage(int index)
    {
        if (hostageUser != null)
        {
            hostageUser.curHostage = false;
        }
        
        hostageUser = userList[index];
        hostageUser.curHostage = true;
        hostageUser.hasHostage = true;
        
        userList.ForEach(x =>
        {
            if(x.hasHostage)
                Debug.Log(userList[index].name + " is Hostage");
        });
    }
    
    #region WordCheck

    public void OpenFrameThis(int count)
    {
        uiThis.OpenFrame(count);
        uiWordCheck.gameObject.SetActive(false);
        
        if (count == 8)
        {
            uiThis.SetFrame8(userList[curUserCount].name);
        }
        else if (count == 9)
        {
            uiThis.SetFrame9(userList[curUserCount].name, userList[curUserCount].jobType);
        }
    }
    
    public void OpenWordCheck()
    {
        uiThis.gameObject.SetActive(false);
        uiWordCheck.gameObject.SetActive(true);

        uiWordCheck.SetLayout(userList[curUserCount].jobType);
        if (userList[curUserCount].jobType == EJobType.Assassin)
        {
            uiWordCheck.SetData(userList.Select(x=>x.name).ToArray());
        }
        else
        {
            uiWordCheck.SetCardData(secretWord);
        }
    }
    
    public void NextWordCheckUser()
    {
        curUserCount++;
        
        if (curUserCount >= userList.Count)
        {
            StartQuestionRound();
        }
        
        uiWordCheck.gameObject.SetActive(false);
        uiThis.gameObject.SetActive(true);
        
        uiThis.OpenFrame(8);
        uiThis.SetFrame8(userList[curUserCount].name);
    }
    
    #endregion

    public void StartQuestionRound()
    {
        curUserCount = 0;
        
        
    }

}
