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
        if(UseAutoNextScreen)
            BindNextScreen<UI_Introduce>();
        
        // 플레이어 데이터를 투표자로 변경한다.
        Managers.Game.SetContext(PlayersDataContext.DataContextType.Voter);
        return true;
    }
}