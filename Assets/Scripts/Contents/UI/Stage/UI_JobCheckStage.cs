using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JobCheckStage : UIStage
{
    public enum Screens
    {
        Sequence01,
        JobIntro01,
        JobInteraction,
        ClockSwitcher,
        NextPlayer
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        return true;
    }
    
    public override void OpenScreen()
    {
        
    }
}
