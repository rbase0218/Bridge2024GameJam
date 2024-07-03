using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISwipe : UIBase
{
    private enum Buttons
    {
        AfterButton,
        BeforeButton
    }

    private enum Texts
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

    protected abstract void SetData();

    protected abstract void OnClickAfterButton();
    protected abstract void OnClickBeforeButton();
    protected abstract void RefreshUI();
}
