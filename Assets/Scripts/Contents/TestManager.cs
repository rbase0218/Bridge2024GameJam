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
    private UserInfo votePointUser;
    private string currentWord;

    private void Start()
    {
        SelectPanel.OnSelectHostage += SetHostage;
        SelectPanel.OnSelectVote += SetVotePoint;
        NextOrderLayer.OnExitLayout += GoJobOpenLayout;
        userInfos = new List<UserInfo>
        {
            new UserInfo(0, "철수", EJobType.Citizen),
            new UserInfo(1, "영희", EJobType.Actor),
            new UserInfo(2, "길동", EJobType.Spy),
        };
        currentWord = Managers.Data.wordArray[Random.Range(0, Managers.Data.wordArray.Length)];
        wordText.text = currentWord;
        StartLayout();
    }

    private void OnDestroy()
    {
        SelectPanel.OnSelectHostage -= SetHostage;
        SelectPanel.OnSelectVote -= SetVotePoint;
        NextOrderLayer.OnExitLayout -= GoJobOpenLayout;
    }
    
    private void SetVotePoint(int index)
    {
        if(votePointUser != null)
        {
            votePointUser.isVotePoint = false;
        }
        
        votePointUser = userInfos[index];
        
        votePointUser.isVotePoint = true;
        
        userInfos.ForEach(x =>
        {
            if (x.isVotePoint)
            {
                Debug.Log(x.name + "님이 투표로 선택되었습니다.");
            }
        });
    }
        
    private void SetHostage(int index)
    {
        if(selectedUser != null)
        {
            selectedUser.curHostage = false;
        }
        
        selectedUser = userInfos[index];

        if (selectedUser.isSelect)
        {
            Debug.Log("이미 선택된 유저입니다.");
            return;
        }

        selectedUser.isSelect = true;
        selectedUser.curHostage = true;
        
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
    
    public void GoJobOpenLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.JobOpen;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }
    
    public void GoWordCheckLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.WordCheck;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void GoNextOrderLayout()
    {
        userCount++;
        Debug.Log(userCount);

        if (userCount >= userInfos.Count)
        {
            currentLayoutIndex = (int)ELayoutName.WordCheck;
            Debug.Log("다음 라운드로 이동." + currentLayoutIndex);
            NextLayout();
            return;
        }

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.NextOrder;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void NextLayout()
    {
        userCount = 0;
        currentLayoutIndex++;
        
        if (currentLayoutIndex >= layouts.Length)
        {
            // 게임 종료
            Debug.Log("게임 종료");
            return;
        }

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }
}