using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Sequence01 : UIScreen
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
        var currentUser = Managers.Game.currentUser;
        
        GetText((int)Texts.Text).SetText(currentUser.userName);

        if (UseAutoNextScreen)
            BindNextScreen<UI_JobIntro01>();
        
        return true;
    }
}
