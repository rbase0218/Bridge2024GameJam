using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher01_Last2 : UIScreen
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
            BindNextScreen<UI_Introduce_Last>();
        
        // 유저 턴 데이터 초기화
        Managers.Game.CleanTurn();
        Managers.Game.ClearYesNoChoices();
        
        return true;
    }
}