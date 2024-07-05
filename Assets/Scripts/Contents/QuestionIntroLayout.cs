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
        var votePoint = users.Find(info => info.isVotePoint);
        List<UserInfo> otherUsers = new List<UserInfo>(users);
        
        if (hostage != null)
        {
            otherUsers.Remove(hostage);
        }
        else
        {
            Debug.LogError("현재 인질이 없습니다.");
        }
        
        if(votePoint != null)
        {
            otherUsers.Remove(votePoint);
        }
        
        var randomUser = otherUsers[Random.Range(0, otherUsers.Count)];
        presentationPanel.SetUserInfo(hostage.name,randomUser.name);
        gameObject.SetActive(true);
    }
    
    public void ActivePanel()
    {
        introText.gameObject.SetActive(false);
        presentationPanel.gameObject.SetActive(true);
    }
}
