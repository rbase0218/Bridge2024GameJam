using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UINameSelect : UIWindow
{
    private enum Buttons
    {
        EntryButton,
        BackButton,
        ManualButton
    }

    private enum UINameField
    {
        NameField_Group
    }
    
    [Space(10), SerializeField]
    private GameObject rootContent;

    private int count = 0;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        Bind<UINameFields>(typeof(UINameField));
        
        GetButton((int)Buttons.EntryButton).onClick.AddListener(OnClickEntryButton);
        GetButton((int)Buttons.BackButton).onClick.AddListener(OnClickBackButton);
        GetButton((int)Buttons.ManualButton).onClick.AddListener(OnClickManualButton);

        return true;
    }

    // 모든 작성이 완료되어서 Entry Button을 누른 경우, 게임 진입한다.
    private void OnClickEntryButton()
    {
        Debug.Log("Click - Entry Button");
        
        // Field의 데이터 값을 가져온다.
        var nameField = Get<UINameFields>((int)UINameField.NameField_Group);
        var fieldList = nameField.GetFields();
        var jobList = GetRandomJobList();
        Managers.Game.ClearUser();
        
        for (int i = 0; i < count; ++i)
        {
            if (String.IsNullOrEmpty(fieldList[i].text))
                fieldList[i].text = Managers.Data.randNicknameArray[i];
            Managers.Game.AddUserInfo(new UserInfo(i, fieldList[i].text, jobList[i]));
        }
        
        nameField.HideAll();
        SceneManager.LoadScene("Ryu-UI Copy");
    }

    // EJobType을 랜덤으로 섞어서 반환한다.
    private List<EJobType> GetRandomJobList()
    {
        List<EJobType> jobTypeEnums = new List<EJobType>();
        
        for (int i = 0; i < count; i++)
        {
            jobTypeEnums.Add(EJobType.VIP);
        }
        switch (count)
        {
            case 3:
            case 4:
                jobTypeEnums.Add(EJobType.Assassin);
                break;
            case 5:
            case 6:
                jobTypeEnums.Add(EJobType.Clown);
                jobTypeEnums.Add(EJobType.Assassin);
                break;
        }
        
        var temp = jobTypeEnums.OrderBy(item => Guid.NewGuid()).ToList();
        jobTypeEnums.Clear();
        jobTypeEnums.AddRange(temp);
        
        return jobTypeEnums;
    }
    
    // 처음 NameModal이 나타난다면, 이름 수에 맞게 데이터를 노출하는 것이 필요.
    public void ExecuteProcess(int count)
    {
        if (count < 0)
            return;
        
        this.count = count;
        Managers.Game.SetGame(count);
        ShowChildren(count);
    }

    private void ShowChildren(int count)
    {
        Get<UINameFields>((int)UINameField.NameField_Group).SetField(count);
    }

    protected override void Clear()
    {
        Get<UINameFields>((int)UINameField.NameField_Group).HideAll();
    }
    
    private void OnClickBackButton()
    {
        Managers.UI.CloseWindow();
    }

    private void OnClickManualButton()
    {
        Managers.UI.ShowWindow<UIManual>();
    }
}
