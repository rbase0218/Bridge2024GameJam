using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    public static Action<int> OnSelectHostage;
    public static Action<int> OnSelectQuestion;
    public static Action<int> OnSelectVote;
    
    [SerializeField] private Button sendButton;
    private GridLayoutGroup gridLayoutGroup;
    private List<SelectButton> buttons;
    private List<UserInfo> users;
    private int selectIndex;
    private bool isEnd;
    
    private void OnValidate()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        buttons = GetComponentsInChildren<SelectButton>(true).ToList();
        gridLayoutGroup.constraintCount = 1;
    }

    private void Start()
    {
        sendButton.interactable = false;
    }

    public void SetButtonLayout(List<UserInfo> userInfos, ESelectType selectType)
    {
        isEnd = false;
        users = userInfos;
        
        for (int i = 0; i < userInfos.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].SetText(userInfos[i].name);
        }

        switch (selectType)
        {
            case ESelectType.Hostage:
                LockButtonForHostage();
                break;
            case ESelectType.Question:
                LockButtonForQuestion();
                break;
            case ESelectType.Vote:
                LockButtonForVote();
                break;
        }
    }
    
    public void PushedButton(int index)
    {
        buttons.ForEach(x=> x.UnSelect());
        buttons[index].Select();
        selectIndex = index;
        sendButton.interactable = true;
    }

    public void SendSelectedHostageIndex()
    {
        if(isEnd) return;
        
        OnSelectHostage?.Invoke(selectIndex);
        ResetButton();
        
        isEnd = true;
    }
    
    public void SendSelectedQuestionIndex()
    {
        if(isEnd) return;
        
        OnSelectQuestion?.Invoke(selectIndex);
        ResetButton();
        
        isEnd = true;
    }
    
    public void SendSelectedVoteIndex()
    {
        if(isEnd) return;
        
        OnSelectVote?.Invoke(selectIndex);
        ResetButton();
        
        isEnd = true;
    }
    
    public void SendRandomSelectedUserIndex()
    {
        if(isEnd) return;
        
        ResetButton();
        List<UserInfo> unSelectUsers = new List<UserInfo>(users);
        unSelectUsers.RemoveAll(x => x.hasHostage);
        unSelectUsers.RemoveAll(x => x.myTurn);
        
        selectIndex = unSelectUsers[UnityEngine.Random.Range(0, unSelectUsers.Count)].index;
        
        OnSelectHostage?.Invoke(selectIndex);
        isEnd = true;
    }
    
    private void ResetButton()
    {
        buttons.ForEach(x=> x.Reset());
        sendButton.interactable = false;
    }
    
    private void LockButtonForHostage()
    {
        List<UserInfo> unSelectUsers = new List<UserInfo>(users);
        unSelectUsers.RemoveAll(x => x.hasHostage == false);
        
        unSelectUsers.ForEach(x =>
        {
            buttons[x.index].gameObject.SetActive(false);
        });
    }

    private void LockButtonForQuestion()
    {
        List<UserInfo> unSelectUsers = new List<UserInfo>(users);
        List<UserInfo> selectUsers = new List<UserInfo>();
        
        foreach (var item in unSelectUsers)
        {
            if(item.curHostage || item.myTurn || item.isDead)
                selectUsers.Add(item);
        }
        
        selectUsers.ForEach(x =>
        {
            buttons[x.index].gameObject.SetActive(false);
        });
    }
    
    private void LockButtonForVote()
    {
        List<UserInfo> unSelectUsers = new List<UserInfo>(users);
        List<UserInfo> selectUsers = new List<UserInfo>();

        foreach (var item in unSelectUsers)
        {
            if(item.isDead)
                selectUsers.Add(item);
        }
        
        selectUsers.ForEach(x =>
        {
            buttons[x.index].gameObject.SetActive(false);
        });
    }
}