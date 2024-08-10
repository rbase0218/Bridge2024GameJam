using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIStage : UIBase
{
    // Stage는 다음 Stage에 대한 정보를 가지고 있다.
    protected UIStage nextStage;

    public Action OnNextStage;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        return true;
    }

    public abstract void OpenScreen();
}