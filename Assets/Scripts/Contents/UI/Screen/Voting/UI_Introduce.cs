using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Introduce : UIScreen
{
    private enum Texts
    {
        Text
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindText(typeof(Texts));
        return true;
    }
    
    protected override bool EnterWindow()
    {
        GetText((int)Texts.Text).SetText(Managers.Game.currentUser.userName);
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_PlayerSelectUIV>();
        
        return true;
    }
}
