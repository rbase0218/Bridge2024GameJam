using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVote : MonoBehaviour
{
    private UIJobGroup _jobGroup;
    private UISelectButtons _selectButtons;

    public void Init()
    {
        _jobGroup = gameObject.GetComponentInChildren<UIJobGroup>();
        _selectButtons = gameObject.GetComponentInChildren<UISelectButtons>();   
    }

    public void OpenFrame()
    {
        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);

    public void SetData(string currUser, params string[] userNames)
    {
        _jobGroup.SetText(currUser);
        _selectButtons.SetData(userNames);
    }
}
