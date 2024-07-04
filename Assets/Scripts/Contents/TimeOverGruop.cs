using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeOverGruop : MonoBehaviour
{
    [SerializeField] private TMP_Text user1Name;
    [SerializeField] private TMP_Text user2Name;
    
    public void SetUserData(List<UserInfo> userInfos, int userCount)
    {
        user1Name.text = userInfos[userCount].name;
        user2Name.text = userInfos[userCount + 1].name;
    }
}
