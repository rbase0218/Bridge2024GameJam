using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher01V : UIScreen
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        Managers.Game.currentUser = Managers.Game._userList[0];
        if(UseAutoNextScreen)
            BindNextScreen<UI_Introduce>();
        return true;
    }
}