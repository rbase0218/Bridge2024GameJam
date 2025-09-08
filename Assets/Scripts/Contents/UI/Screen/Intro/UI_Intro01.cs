using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Intro01 : UIScreen
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        _gauge.SetGauge(3f);

        if (UseAutoNextScreen)
            BindNextScreen<UI_Sequence01>();
        
        // 유저 데이터 설정 => Normal
        Managers.Game.SetContext(PlayersDataContext.DataContextType.Normal);
        return true;
    }
}
