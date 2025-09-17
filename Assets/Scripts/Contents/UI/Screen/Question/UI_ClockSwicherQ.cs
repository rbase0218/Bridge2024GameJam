using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClockSwitcherQ : UIScreen
{
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        Managers.Sound.PlaySFX("Clock");
        _gauge.SetGauge(3f);
        if (UseAutoNextScreen)
            BindNextScreen<UI_Switcher01V>();
        
        return true;
    }
}
