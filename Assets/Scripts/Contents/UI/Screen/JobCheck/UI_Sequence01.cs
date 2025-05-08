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
        var currentPlayerName = Managers.Game.GetCurrentPlayer().userName;
        
        GetText((int)Texts.Text).SetText(currentPlayerName);
        
        if (UseAutoNextScreen)
            BindNextScreen<UI_JobIntro01>();
        
        return true;
    }
}
