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
        if (UseAutoNextScreen)
            BindNextScreen<UI_Sequence01>();
        
        // 유저 데이터 타입을 질문으로 설정
        Managers.Game.SetContext(PlayersDataContext.DataContextType.Questioner);
        return true;
    }
}
