using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIReturnMain : UIWindow
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
        SceneManager.LoadScene(0);
    }

    private void OnClickNoButton()
    {
        Managers.UI.CloseWindow();
    }
}
