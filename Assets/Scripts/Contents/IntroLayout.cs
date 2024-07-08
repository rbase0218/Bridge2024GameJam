using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroLayout : MonoBehaviour,ILayoutControl
{
    [SerializeField] private Button imageButton;
    public void ExitLayout()
    {
        imageButton.interactable = false;
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        imageButton.interactable = true;
        gameObject.SetActive(true);
    }
}