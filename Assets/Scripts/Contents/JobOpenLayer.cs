using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobOpenLayer : MonoBehaviour, ILayoutControl
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text jobText;
    [SerializeField] private GameObject card;
    [SerializeField] private Button cardButton;
    
    public void ExitLayout()
    {
        cardButton.interactable = false;
        card.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartLayout()
    {
        gameObject.SetActive(true);
    }

    public void SetUserData(List<UserInfo> users, int userCount)
    {
        nameText.text = users[userCount].name;
        jobText.text = Utils.ChangeEnum(users[userCount].jobType);
    }
    

    public void OnClickSendButton()
    {
        cardButton.interactable = true;
        card.SetActive(false);
    }
}   