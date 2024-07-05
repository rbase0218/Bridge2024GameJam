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

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        selectPanel.SetButtonLayout(users, users.Count);
        gameObject.SetActive(true);
    }

    public void SetPanel(List<UserInfo> users)
    {
        selectPanel.SetButtonLayout(users, users.Count);
    }
}