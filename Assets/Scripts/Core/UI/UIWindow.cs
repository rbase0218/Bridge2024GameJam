using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWindow : UIBase
{
    protected bool alwaysOpen = false;
    protected virtual void Setting() { }
    protected virtual void Clear() { }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        Managers.UI.RegisterWindow(this);
        Hide();
        
        return true;
    }
    
    public void Open()
    {
        Setting();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (alwaysOpen)
            return;

        Clear();
        gameObject.SetActive(false);
    }

}