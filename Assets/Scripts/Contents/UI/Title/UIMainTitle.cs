using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainTitle : UIWindow
{
    private enum Buttons
    {
        BackButton,
        //HomeButton,
        ManualButton,
        StartButton,
        SettingButton,
        ExitButton
    }
    
    protected override bool Init()
    {
        // Main Title은 항상 UI를 보여주고 있는 상태를 가진다.
        alwaysOpen = true;
        
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.StartButton).onClick.AddListener(OnClickStartButton);
        GetButton((int)Buttons.SettingButton).onClick.AddListener(OnClickSettingButton);
        GetButton((int)Buttons.ExitButton).onClick.AddListener(OnClickExitButton);
        
        GetButton((int)Buttons.BackButton).onClick.AddListener(OnClickBackButton);
        //GetButton((int)Buttons.HomeButton).onClick.AddListener(OnClickHomeButton);
        GetButton((int)Buttons.ManualButton).onClick.AddListener(OnClickManualButton);
        
        return true;
    }
    protected override bool EnterWindow()
    {
        return true;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnClickBackButton();
    }
    
    // 게임 실행에 직접적으로 영향을 끼치는 Button들
    private void OnClickStartButton()
    {
        Managers.Sound.PlaySFX("Click");
        Managers.UI.ShowWindow<UIStartProcess>();
    }
    
    private void OnClickSettingButton()
    {
        Managers.Sound.PlaySFX("Click");
        var setting = Managers.UI.ShowWindow<UIGameSetting>();
        setting.OnClickButtons(() =>
        {
            // + (2024/08/19) SoundManager Error로 인한 주석처리
            setting.Save();
            Managers.Sound.PlaySFX("Click");
            Managers.UI.CloseWindow();
        });
    }
    
    private void OnClickExitButton()
    {
        Managers.Sound.PlaySFX("Click");
        var exit = Managers.UI.ShowWindow<UIGameExit>();
        exit.OnClickButtons(() =>
        {
            Managers.Sound.PlaySFX("Click");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        });
    }
    
    // 상단 부분에 있는 Button들
    private void OnClickManualButton()
    {
        Managers.Sound.PlaySFX("Click");
        Managers.UI.ShowWindow<UIManual>();
    }
    
    private void OnClickHomeButton()
    {
        Managers.Sound.PlaySFX("Click");
        var returnMain = Managers.UI.ShowWindow<UIReturnMain>();
        returnMain.OnClickButtons(() =>
        {
            Managers.Sound.PlaySFX("Click");
            Debug.Log("메인으로 돌아감");
        });
    }
    
    private void OnClickBackButton()
    {
        Managers.Sound.PlaySFX("Click");

        if(!Managers.UI.isEmptyWindow())
            Managers.UI.CloseWindow();
        else
        {
            var exit = Managers.UI.ShowWindow<UIGameExit>();
            exit.OnClickButtons(() =>
            {
                Managers.Sound.PlaySFX("Click");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            });
        }
    }
}
