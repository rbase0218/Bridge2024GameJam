using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NextPlayer : UIScreen
{
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        return true;
    }

    protected override bool EnterWindow()
    {
        // 유저 리스트를 가져온다.
        // 리스트의 끝 인덱스에 도달했는지 확인한다.
        if (Managers.Game.NextUser())
        {
            if (UseAutoNextScreen)
                BindNextScreen<UI_Sequence01>();
        }
        else
        {
            if (UseAutoNextScreen)
                BindNextScreen<UI_Switcher01>();
        }

        return true;
    }
}

