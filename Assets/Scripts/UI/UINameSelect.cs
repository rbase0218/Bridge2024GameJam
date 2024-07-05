using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINameSelect : UIWindow
{
    private enum Buttons
    {
        EntryButton
    }

    private enum RectTransforms
    {
        Content
    }

    private enum UINameFields
    {
        NameField_List
    }
    
    [Space(10), SerializeField]
    private GameObject rootContent;

    private int count = 0;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        Bind<RectTransform>(typeof(RectTransforms));
        Bind<UINameField>(typeof(UINameFields));
        
        GetButton((int)Buttons.EntryButton).onClick.AddListener(OnClickEntryButton);

        return true;
    }

    protected override void Setting()
    {
        Get<RectTransform>((int)RectTransforms.Content).anchoredPosition = Vector2.zero;
    }

    // 모든 작성이 완료되어서 Entry Button을 누른 경우, 게임 진입한다.
    private void OnClickEntryButton()
    {
        Debug.Log("Click - Entry Button");
        
        // Field의 데이터 값을 가져온다.
        var nameField = Get<UINameField>((int)UINameFields.NameField_List);
        var fieldList = nameField.GetFields();
        Managers.Game.ClearUser();
        
        // Feature List
        // - Empty Name -> Add Nickname
        // - Duplicate -> Not Ok
        for (int i = 0; i < count; ++i)
        {
            if (String.IsNullOrEmpty(fieldList[i].text))
                fieldList[i].text = Managers.Data.randNicknameArray[i];

            if (!Managers.Game.AddUserInfo(new UserInfo(i, fieldList[i].text)))
            {
                // 중복 비허용 Window 추가 예정
                return;
            }
        }
        
        Managers.UI.CloseWindow();
    }
    
    // 처음 NameModal이 나타난다면, 이름 수에 맞게 데이터를 노출하는 것이 필요.
    public void ExecuteProcess(int count)
    {
        if (count < 0)
            return;
        
        ShowChildren(count);
    }

    private void ShowChildren(int count)
    {
        for (int i = 0; i < rootContent.transform.childCount; ++i)
        {
            if(i < count)
                rootContent.transform.GetChild(i).gameObject.SetActive(true);
            else
                rootContent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
