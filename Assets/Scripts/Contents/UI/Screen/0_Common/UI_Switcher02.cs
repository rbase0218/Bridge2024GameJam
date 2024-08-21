using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher02 : UIScreen
{
    private enum Buttons
    {
        NextButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        return true;
    }
    
    protected override bool EnterWindow()
    {
        if (UseAutoNextScreen)
            BindNextScreen<UI_Switcher01V>();
        
        Managers.Game.SetCanAllQuestion();
        return true;
    }
    
    private void OnClickNextButton()
    {
        OnNextScreen<UI_Switcher01V>();
    }
}
