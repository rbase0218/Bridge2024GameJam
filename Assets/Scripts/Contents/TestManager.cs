using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestManager : MonoBehaviour
{
    [SerializeField] private GameObject[] layouts;
    [SerializeField] private TMP_Text wordText;

    private List<UserInfo> userInfos;
    private GameObject currentLayout;
    private int currentLayoutIndex;
    private int userCount;
    private UserInfo selectedUser;
    private string currentWord;

    private void Start()
    {
        SelectPanel.OnSelectUser += GetSelectedUser;
        NextOrderLayer.OnExitLayout += NextLayout;
        userInfos = Managers.Game._saveUserInfoList;

        var categoryWord = Managers.Data.categoryArray[Managers.Game.currCategoryIndex];
        var len = Managers.Data.categoryDic[categoryWord].Length;
        currentWord = Managers.Data.categoryDic[categoryWord][Random.Range(0, len)];
        
        wordText.text = currentWord;
        StartLayout();
    }

    private void OnDestroy()
    {
        SelectPanel.OnSelectUser -= GetSelectedUser;
        NextOrderLayer.OnExitLayout -= NextLayout;
    }

    public void GetSelectedUser(int index)
    {
        selectedUser = userInfos[index];

        if (selectedUser.isSelect)
        {
            Debug.Log("이미 선택된 유저입니다.");
            return;
        }

        selectedUser.isSelect = true;

        userInfos.ForEach(x =>
        {
            if (x.isSelect)
            {
                Debug.Log(x.name + "님이 인질로 선택되었습니다.");
            }
        });
    }

    public void StartLayout()
    {
        userCount = 0;
        currentLayoutIndex = 0;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
        userCount++;
    }

    public void RepeatLayout()
    {
        if (userCount >= userInfos.Count)
        {
            userCount = 0;
            NextLayout();
        }

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
        userCount++;
    }

    public void NextLayout()
    {
        if (currentLayoutIndex >= layouts.Length - 1)
        {
            // 게임 종료
            return;
        }

        userCount = 0;
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();

        currentLayoutIndex++;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }
}