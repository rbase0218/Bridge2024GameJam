using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGameSetting : UIWindow
{
    private enum Buttons
    {
        BackButton,
        ManualButton,
        EmergencyButton,
        InfoButton
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
        GetButton((int)Buttons.InfoButton).onClick.AddListener(OnClickInfoButton);
        HideEmergencyButton();
        HideInfoButton();
        return true;
    }
    protected override bool EnterWindow()
    {
        HideEmergencyButton();
        HideInfoButton();
        return true;
    }

    // 상단 부분에 있는 Button들
    private void OnClickManualButton()
    {
        Time.timeScale = 0;
        Managers.Sound.PlaySFX("Click");
        Managers.UI.ShowWindow<UIManual>();
    }

    private void OnClickBackButton()
    {
        Time.timeScale = 0;
        Managers.Sound.PlaySFX("Click");

        var exit = Managers.UI.ShowWindow<UIGameExit>();
        exit.OnClickButtons(() =>
        {
            Managers.Sound.PlaySFX("Click");
            Managers.Ads.ShowAd();
        }, () =>
        {
            Time.timeScale = 1;
            Managers.Sound.PlaySFX("Click");
            Managers.UI.CloseWindow();
        });
    }

    private void OnClickEmergencyButton()
    {
        Time.timeScale = 0;
        Managers.Sound.PlaySFX("Click");

        var exit = Managers.UI.ShowWindow<UIEmergency>();
        exit.OnClickButtons(() =>
        {
            Time.timeScale = 1;
            Managers.Sound.PlaySFX("Click");
            var gauge = FindObjectOfType<UI_Gauge>();
            gauge?.Stop();
            Managers.UI.CloseWindow();
            Managers.UI.CloseWindow();
            Managers.UI.ShowWindow<UI_PlayerSelectUIV_Proposer>();
        }, () =>
        {
            Time.timeScale = 1;
            Managers.Sound.PlaySFX("Click");
            Managers.UI.CloseWindow();
        });
    }

    private void OnClickInfoButton()
    {
        Time.timeScale = 0;
        Managers.Sound.PlaySFX("Click");
        Managers.UI.ShowWindow<UIInfo>().RefreshData();
    }

    public void ShowEmergencyButton()
    {
        GetButton((int)Buttons.EmergencyButton).gameObject.SetActive(true);
    }

    public void HideEmergencyButton()
    {
        GetButton((int)Buttons.EmergencyButton).gameObject.SetActive(false);
    }

    public void ShowInfoButton()
    {
        GetButton((int)Buttons.InfoButton).gameObject.SetActive(true);
    }

    public void HideInfoButton()
    {
        GetButton((int)Buttons.InfoButton).gameObject.SetActive(false);
    }
}