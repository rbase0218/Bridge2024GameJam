using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClockSwitcher : UIScreen
{
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        return true;
    }
}