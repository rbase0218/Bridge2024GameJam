using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher01 : UIScreen
{
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        if(UseAutoNextScreen)
            BindNextScreen<UI_Sequence02>();
        
        // 플레이어 데이터를 질문자로 변경한다.
        Managers.Game.SetContext(PlayersDataContext.DataContextType.Questioner);
        
        return true;
    }
}


