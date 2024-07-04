using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public enum EJobType
{
    None,
    Spy,
    Actor
}

[Serializable]

public class UserInfo
{
    public int index;
    public string name;
    public EJobType jobType;
    public bool isSelect;
    
    public UserInfo(int index, string name, EJobType jobType)
    {
        this.index = index;
        this.name = name;
        this.jobType = jobType;
        isSelect = false;
    }
}

public class SelectPanel : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;
    private List<SelectButton> buttons;
    private int selectIndex;
    
    private void OnValidate()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        buttons = GetComponentsInChildren<SelectButton>(true).ToList();
        gridLayoutGroup.constraintCount = 1;
    }
    
    public void SetButtonLayout(List<UserInfo> userInfos,int count = 3)
    {
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
        buttons.ForEach(x=> x.Reset());
        buttons[index].Select();
        selectIndex = index;
    }

    public int GetSelectedUserIndex()
    {
        return selectIndex;
    }
}
