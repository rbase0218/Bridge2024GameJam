using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManual : UIWindow
{
    private enum Buttons
    {
        CloseButton,
        BeforeButton,
        AfterButton
    }

    public UIBoardSelect boardSelect;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.CloseButton).onClick.AddListener(OnClickCloseButton);
        GetButton((int)Buttons.AfterButton).onClick.AddListener(OnClickAfterButton);
        GetButton((int)Buttons.BeforeButton).onClick.AddListener(OnClickBeforeButton);

        return true;
    }

    private void OnClickCloseButton()
    {
        Managers.UI.CloseWindow();
    }

    private void OnClickAfterButton()
    {
        boardSelect.SetBoard(1);
    }

    private void OnClickBeforeButton()
    {
        boardSelect.SetBoard(-1);
    }
}
