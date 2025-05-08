using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Introduce : UIScreen
{
    private enum Texts
    {
        Text
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
        // GetText((int)Texts.Text).SetText(Managers.Game.currentUser.userName);
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_PlayerSelectUIV>();
        
        return true;
    }
    
    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");
        OnNextScreen<UI_PlayerSelectUIV>();
    }
}