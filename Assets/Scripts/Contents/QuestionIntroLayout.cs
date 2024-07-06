using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuestionIntroLayout : MonoBehaviour,ILayoutControl
{
    [SerializeField] private TMP_Text introText;
    [SerializeField] private Button imageButton;
    [SerializeField] private PresentationPanel presentationPanel;
    
    public void ExitLayout()
    {
        imageButton.interactable = false;
        presentationPanel.gameObject.SetActive(false);
        introText.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        imageButton.interactable = true;
        var hostage = users.Find(info => info.curHostage);
        
        presentationPanel.SetUserInfo(hostage.name,curUser.name);
        gameObject.SetActive(true);
    }
    
    public void ActivePanel()
    {
        introText.gameObject.SetActive(false);
        presentationPanel.gameObject.SetActive(true);
    }
}
