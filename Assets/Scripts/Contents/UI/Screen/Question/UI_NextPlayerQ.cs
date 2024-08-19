using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NextPlayerQ : UIScreen
{
    private enum Texts
    {
        NameA,
        NameB
    }

    private enum Buttons
    {
        NextButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        if(UseAutoNextScreen)
            BindNextScreen<UI_Switcher02>();
        
        return true;
    }
    
    private void OnClickNextButton()
    {
        OnNextScreen<UI_Switcher02>();
    }
}
