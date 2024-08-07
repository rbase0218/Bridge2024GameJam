using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWindow : UIBase
{
    protected bool alwaysOpen = false;
    public bool IsAlwaysOpen => alwaysOpen;
    protected abstract bool EnterWindow();
    protected virtual void ExitWindow() { }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        // 실행 되었을 경우, 해당 Window를 등록한다.
        Managers.UI.RegisterWindow(this);
        // 등록 처리 후, Window를 숨긴다.
        Hide();
        
        return true;
    }
    
    public void Open()
    {
        // Window가 열릴 때 데이터 처리 작업을 위한 메서드
        var isOpen = EnterWindow();
        if (isOpen == false)
            Debug.Log($"{GetType()} Window Open Failed.");
        
        gameObject.SetActive(isOpen);
    }

    public void Hide(bool isForced = false)
    {
        if (alwaysOpen && isForced == false)
            return;
        
        ExitWindow();
        gameObject.SetActive(false);
    }

    // 강제 숨김 처리
    public void ForcedHide()
    {
        Hide(true);
    }

}