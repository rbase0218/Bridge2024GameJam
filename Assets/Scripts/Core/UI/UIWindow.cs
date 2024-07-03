using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWindow : UIBase
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        return true;
    }
    
    public void Open() => gameObject.SetActive(true);
    public void Hide()
    {
        if (_alwaysOpen)
            return;
        
        gameObject.SetActive(false);
    }


    protected virtual void Awake()
    {
        Managers.UI.AddWindow(this);
    }
}