using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        return true;
    }
    protected override bool EnterWindow()
    {
        return true;
    }

    public void OnClickButtons(UnityAction yesButton, UnityAction noButton = null)
    {
        GetButton((int)Buttons.YesButton).onClick.RemoveAllListeners();
        GetButton((int)Buttons.NoButton).onClick.RemoveAllListeners();

        GetButton((int)Buttons.YesButton).onClick.AddListener(yesButton);
        if (noButton == null)
        {
            GetButton((int)Buttons.NoButton).onClick.AddListener(() =>
            {
                Managers.UI.CloseWindow();
            });
        }
        else
            GetButton((int)Buttons.NoButton).onClick.AddListener(noButton);
    }
}