using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher01VR : UIScreen
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        if(UseAutoNextScreen)
            BindNextScreen<UI_VoteResult>();
        
        return true;
    }
}