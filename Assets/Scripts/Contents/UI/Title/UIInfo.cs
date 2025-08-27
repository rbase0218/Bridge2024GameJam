using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfo : UIWindow
{
    private enum Buttons
    {
        BeforeButton,
        AfterButton,
        CloseButton
    }

    private enum Objects
    {
        First,
        Second
    }

    private int index;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));
        
        GetButton((int)Buttons.BeforeButton).onClick.AddListener(OnClickBeforeButton);
        GetButton((int)Buttons.AfterButton).onClick.AddListener(OnClickAfterButton);
        GetButton((int)Buttons.CloseButton).onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            Managers.Sound.PlaySFX("Click");
            Managers.UI.CloseWindow();
        });
        
        return true;
    }
    protected override bool EnterWindow()
    {
        return true;
    }
    
    private void OnClickBeforeButton()
    {
        Managers.Sound.PlaySFX("Click");

        if (index == 0)
            return;
        
        index--;
        Managers.UI.ShowWindow<UIInfo_Players>(true).RefreshData();
        Managers.UI.CloseWindow<UIInfo_Questions>();
    }
    
    private void OnClickAfterButton()
    {
        Managers.Sound.PlaySFX("Click");

        if (index == 1)
            return;
        
        index++;
        Managers.UI.ShowWindow<UIInfo_Questions>(true).RefreshData();
        Managers.UI.CloseWindow<UIInfo_Players>();
    }

    public void RefreshData()
    {
        GetObject((int)Objects.First).SetActive(true);
        GetObject((int)Objects.Second).SetActive(false);
        GetObject((int)Objects.First).GetComponent<UIInfo_Players>().RefreshData();
        GetObject((int)Objects.Second).GetComponent<UIInfo_Questions>().RefreshData();
        index = 0;
    }
}