using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeOverGruop : MonoBehaviour
{
    [SerializeField] private TMP_Text user1Name;
    [SerializeField] private TMP_Text user2Name;
    
    public void SetUserData(List<UserInfo> userInfos, int userCount)
    {
        user1Name.text = userInfos[userCount].name;
        
        if (userCount + 1 >= userInfos.Count)
        {
            user2Name.text = userInfos[0].name;
            return;
        }
        user2Name.text = userInfos[userCount + 1].name;
    }
    
    public void SetUserDataForNextQuestion(UserInfo user1, string user2)
    {
        user1Name.text = user1.name;
        
        if(TestManager.instance.isQuestionEnd)
        {
            user2Name.text = "종료";
            return;
        }
        user2Name.text = user2;
    }
}