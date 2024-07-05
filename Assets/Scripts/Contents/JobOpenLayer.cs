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

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        nameText.text = curUser.name;
        jobText.text = Utils.ChangeEnum(curUser.jobType);
        gameObject.SetActive(true);
    }

    public void OnClickSendButton()
    {
        cardButton.interactable = true;
        card.SetActive(false);
    }
}   