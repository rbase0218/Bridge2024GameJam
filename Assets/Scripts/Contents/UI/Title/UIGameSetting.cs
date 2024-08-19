using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIGameSetting : UIWindow
{
    private enum Buttons
    {
        SaveButton,
        ExitButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));

        return true;
    }
    protected override bool EnterWindow()
    {
        return true;
    }
    
    public void OnClickButtons(UnityAction yesButton, UnityAction noButton = null)
    {
        GetButton((int)Buttons.SaveButton).onClick.RemoveAllListeners();
        GetButton((int)Buttons.ExitButton).onClick.RemoveAllListeners();
        
        GetButton((int)Buttons.SaveButton).onClick.AddListener(yesButton);
        if (noButton == null)
        {
            GetButton((int)Buttons.ExitButton).onClick.AddListener(() =>
            {
                Managers.UI.CloseWindow();
            });
        }
        else
            GetButton((int)Buttons.ExitButton).onClick.AddListener(noButton);
    }
}