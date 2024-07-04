using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] SelectPanel selectPanel;
    private List<UserInfo> userInfos;
    private void Start()
    {
        userInfos = new List<UserInfo>
        {
            new UserInfo(0, "철수", EJobType.None),
            new UserInfo(1, "영희", EJobType.None),
            new UserInfo(2, "길동", EJobType.None),
            new UserInfo(3, "동", EJobType.None),
            new UserInfo(4, "길", EJobType.None),
            new UserInfo(5, "길동동", EJobType.None),
        };
        
        selectPanel.SetButtonLayout(userInfos, userInfos.Count);
    }

    public void GetSelectedUser()
    {
        Debug.Log(userInfos[selectPanel.GetSelectedUserIndex()].name);
    }
}
