using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVote : MonoBehaviour
{
    private UIJobGroup _jobGroup;
    private UISelectButtons _selectButtons;

    private void Awake()
    {
        _jobGroup = gameObject.GetComponentInChildren<UIJobGroup>();
        _selectButtons = gameObject.GetComponentInChildren<UISelectButtons>();
        
        SetData("강호동", "백종원", "이만기", "카리나", "누구냐", "강호동");
    }

    public void SetData(string currUser, params string[] userNames)
    {
        _jobGroup.SetText(currUser);
        _selectButtons.SetData(userNames);
    }
}
