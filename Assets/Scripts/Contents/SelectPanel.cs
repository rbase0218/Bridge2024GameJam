using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    public static Action<int> OnSelectUser;
    
    [SerializeField] private Button sendButton;
    private GridLayoutGroup gridLayoutGroup;
    private List<SelectButton> buttons;
    private List<UserInfo> users;
    private int selectIndex;
    
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

    public void SetButtonLayout(List<UserInfo> userInfos,int count = 3)
    {
        users = userInfos;
        if (count == 3 || count == 5)
        {
            gridLayoutGroup.constraintCount = 1;
        }
        else
        {
            gridLayoutGroup.constraintCount = 2;
        }

        for (int i = 0; i < count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].SetText(userInfos[i].name);
        }
    }
    
    public void PushedButton(int index)
    {
        buttons.ForEach(x=> x.UnSelect());
        buttons[index].Select();
        selectIndex = index;
        sendButton.interactable = true;
    }

    public void SendSelectedUserIndex()
    {
        ResetButton();
        OnSelectUser?.Invoke(selectIndex);
    }
    
    public void SendRandomSelectedUserIndex()
    {
        ResetButton();
        selectIndex = users.FindIndex(x => x.isSelect == false);
        OnSelectUser?.Invoke(selectIndex);
    }
    
    public void ResetButton()
    {
        buttons.ForEach(x=> x.Reset());
        buttons[selectIndex].Lock();
        sendButton.interactable = false;
    }
}
