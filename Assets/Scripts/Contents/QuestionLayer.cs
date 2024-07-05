using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuestionLayer : MonoBehaviour, ILayoutControl, IUserData
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private SelectLayer selectLayer;
    
    public List<UserInfo> Users { get; set; }
    public UserInfo CurtUser { get; set; }
    
    public void ExitLayout()
    {
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        Users = users;
        CurtUser = curUser;
        selectLayer.SetPanel(users);
        nameText.text = CurtUser.name;
        gameObject.SetActive(true);
    }
}
