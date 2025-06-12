using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGameSetting : UIWindow
{
    private enum Buttons
    {
        BackButton,
        ManualButton,
        EmergencyButton
    }

    protected override bool Init()
    {
        alwaysOpen = true;

        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.BackButton).onClick.AddListener(OnClickBackButton);
        GetButton((int)Buttons.ManualButton).onClick.AddListener(OnClickManualButton);
        GetButton((int)Buttons.EmergencyButton).onClick.AddListener(OnClickEmergencyButton);

        return true;
    }
    protected override bool EnterWindow()
    {
        return true;
    }

    // 상단 부분에 있는 Button들
    private void OnClickManualButton()
    {
        Managers.Sound.PlaySFX("Click");
        Managers.UI.ShowWindow<UIManual>();
    }

    private void OnClickBackButton()
    {
        Managers.Sound.PlaySFX("Click");

        var exit = Managers.UI.ShowWindow<UIGameExit>();
        exit.OnClickButtons(() =>
        {
            Managers.Sound.PlaySFX("Click");
        });
    }

    private void OnClickEmergencyButton()
    {
        Managers.Sound.PlaySFX("Click");
        //Managers.UI.ShowWindow<UIGameEmergency>();
    }
}