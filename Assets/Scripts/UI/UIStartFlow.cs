using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartFlow : UIWindow
{
    private enum Buttons
    {
        NextButton,
        //ExitButton
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }

    private void OnClickNextButton()
    {
        Debug.Log("Click - Next Button");
        
        // Next UI 띄어주기~
    }
}
