using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLayout : MonoBehaviour,ILayoutControl
{
    public void ExitLayout()
    {
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        gameObject.SetActive(true);
    }
}
