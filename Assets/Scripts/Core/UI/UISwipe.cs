using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISwipe : UIBase
{
    protected enum Buttons
    {
        AfterButton,
        BeforeButton
    }

    protected enum Texts
    {
        SwipeValue
    }

    protected int count;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.AfterButton).onClick.AddListener(OnClickAfterButton);
        GetButton((int)Buttons.BeforeButton).onClick.AddListener(OnClickBeforeButton);
        
        return true;
    }

    public int GetCount()
    {
        return count;
    }

    protected abstract void OnClickAfterButton();
    protected abstract void OnClickBeforeButton();
    protected abstract void RefreshUI();
    
}
