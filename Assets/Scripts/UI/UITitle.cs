using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitle : UIWindow
{
    private enum Buttons
    {
        StartButton,
        SettingButton,
        ExitButton,
        BackButton,
        HomeButton,
        ManualButton
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartButton).onClick.AddListener(OnClickStartButton);
        GetButton((int)Buttons.SettingButton).onClick.AddListener(OnClickSettingButton);
        GetButton((int)Buttons.ExitButton).onClick.AddListener(OnClickExitButton);
        GetButton((int)Buttons.HomeButton).onClick.AddListener(OnClickHomeButton);
        GetButton((int)Buttons.BackButton).onClick.AddListener(OnClickBackButton);
        GetButton((int)Buttons.ManualButton).onClick.AddListener(OnClickManualButton);
        
        // Sound Manager
        
        return true;
    }

    private void OnClickStartButton()
    {
        // Start Modal을 띄운다.
        Debug.Log("Click - Start");
        Managers.UI.ShowWindow<UIStartFlow>();
    }

    private void OnClickSettingButton()
    {
        Debug.Log("Click - Setting");

        Managers.UI.ShowWindow<UISettings>();
    }

    private void OnClickExitButton()
    {
        Debug.Log("Click - Exit");
        Managers.UI.ShowWindow<UIGameExit>();
    }

    private void OnClickBackButton()
    {
        Managers.UI.CloseWindow();
    }

    private void OnClickHomeButton()
    {
        Managers.UI.ShowWindow<UIReturnMain>();
    }

    private void OnClickManualButton()
    {
        Managers.UI.ShowWindow<UIManual>();
    }
}
