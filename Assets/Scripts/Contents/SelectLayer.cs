using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLayer : MonoBehaviour, ILayoutControl
{
    [SerializeField] private SelectPanel selectPanel;
    
    public void ExitLayout()
    {
        gameObject.SetActive(false);
    }

    public void StartLayout()
    {
        gameObject.SetActive(true);
    }

    public void SetUserData(List<UserInfo> users, int userCount)
    {
        selectPanel.SetButtonLayout(users, users.Count);
    }
}