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
        selectPanel.SetButtonLayoutForSpy(users, users.Count);
        gameObject.SetActive(true);
    }

    public void SetPanelForSpy(List<UserInfo> users)
    {
        selectPanel.SetButtonLayoutForSpy(users, users.Count);
    }
    
    public void SetPanel(List<UserInfo> users)
    {
        selectPanel.SetButtonLayout(users, users.Count);
    }
    
    public void SetRandomSelectedUserIndex()
    {
        selectPanel.SendRandomSelectedUserIndex();
    }
}