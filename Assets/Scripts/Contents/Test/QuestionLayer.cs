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
    [SerializeField] private GameObject inputField;
    [SerializeField] private TMP_Text inputFieldText;
    
    public List<UserInfo> Users { get; set; }
    public UserInfo CurtUser { get; set; }
    
    public void ExitLayout()
    {
        gameObject.SetActive(false);
        selectLayer.gameObject.SetActive(false);
        inputField.SetActive(true);
        CurtUser.myTurn = false;
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        inputField.GetComponentInChildren<TMP_InputField>().text = "";

        Users = users;
        CurtUser = curUser;
        
        CurtUser.myTurn = true;
        
        nameText.text = CurtUser.name;
        selectLayer.SetPanelLayout(users, ESelectType.Question);
        gameObject.SetActive(true);
    }
}
