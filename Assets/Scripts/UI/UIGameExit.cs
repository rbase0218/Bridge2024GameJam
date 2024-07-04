using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameExit : UIWindow
{
    private enum Buttons
    {
        YesButton,
        NoButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.YesButton).onClick.AddListener(OnClickYesButton);
        GetButton((int)Buttons.NoButton).onClick.AddListener(OnClickNoButton);
        
        return true;
    }

    private void OnClickYesButton()
    {
        // Game Exit 처리
    }

    private void OnClickNoButton()
    {
        Managers.UI.CloseWindow();
    }
}
