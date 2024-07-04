using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] private GameObject[] layouts;
    [SerializeField] private SelectPanel selectPanel;
    private List<UserInfo> userInfos;
    private GameObject currentLayout;
    private int currentLayoutIndex;
    
    private void Start()
    {
        userInfos = new List<UserInfo>
        {
            new UserInfo(0, "철수", EJobType.None),
            new UserInfo(1, "영희", EJobType.None),
            new UserInfo(2, "길동", EJobType.None),
            new UserInfo(3, "동", EJobType.None),
            new UserInfo(4, "길", EJobType.Spy),
            new UserInfo(5, "금희", EJobType.Actor),
        };
        
        selectPanel.SetButtonLayout(userInfos, userInfos.Count);
        StartLayout();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            NextLayout();
        }
    }

    public void GetSelectedUser()
    {
        Debug.Log(userInfos[selectPanel.GetSelectedUserIndex()].name);
    }
    
    public void StartLayout()
    {
        currentLayoutIndex = 0;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout();
    }
    
    public void RepeatLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout();
    }
    
    public void NextLayout()
    {
        if(currentLayoutIndex >= layouts.Length - 1)
        {
            // 게임 종료
            return;
        }
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        
        currentLayoutIndex++;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout();
    }
}