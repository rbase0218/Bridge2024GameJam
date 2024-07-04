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

    [SerializeField]
    private GameObject childObj;

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

    public void ShowChildren(int count)
    {
        if (count < 0)
            return;

        for (int i = 0; i < rootContent.transform.childCount; ++i)
        {
            if(i < count)
                rootContent.transform.GetChild(i).gameObject.SetActive(true);
            else
                rootContent.transform.GetChild(i).gameObject.SetActive(false);
        }

        this.count = count;
    }

    private void OnClickEntryButton()
    {
        Debug.Log("Click - Entry Button");
        Managers.UI.CloseWindow();
        
        var nameField = Get<UINameField>((int)UINameFields.NameField_List);
        nameField.RefreshField();
        Managers.Game.Reset();
        
        for (int i = 0; i < count; ++i)
        {
            Managers.Game.AddUserInfo(new UserInfo(
                i,
                nameField._inputFields[i].text
            ));
        }
    }
}
