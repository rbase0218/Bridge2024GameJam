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
        gameObject.SetActive(true);
    }

    public void SetPanelLayout(List<UserInfo> users, ESelectType selectType)
    {
        selectPanel.SetButtonLayout(users, selectType);
    }
    
    public void SetRandomSelectedUserIndex()
    {
        selectPanel.SendRandomSelectedUserIndex();
    }
}