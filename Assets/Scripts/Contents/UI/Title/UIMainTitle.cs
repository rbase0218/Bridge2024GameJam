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
    
    // 게임 실행에 직접적으로 영향을 끼치는 Button들
    private void OnClickStartButton()
    {
        Managers.UI.ShowWindow<UIStartProcess>();
    }
    
    private void OnClickSettingButton()
    {
        var setting = Managers.UI.ShowWindow<UIGameSetting>();
        setting.OnClickButtons(() =>
        {
            // + (2024/08/19) SoundManager Error로 인한 주석처리
            //setting.Save();
            
            Managers.UI.CloseWindow();
        });
    }
    
    private void OnClickExitButton()
    {
        var exit = Managers.UI.ShowWindow<UIGameExit>();
        exit.OnClickButtons(() =>
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        });
    }
    
    // 상단 부분에 있는 Button들
    private void OnClickManualButton()
    {
        Managers.UI.ShowWindow<UIManual>();
    }
    
    private void OnClickHomeButton()
    {
        var returnMain = Managers.UI.ShowWindow<UIReturnMain>();
        returnMain.OnClickButtons(() =>
        {
            Debug.Log("메인으로 돌아감");
        });
    }
    
    private void OnClickBackButton()
    {
        Managers.UI.CloseWindow();
    }
}
