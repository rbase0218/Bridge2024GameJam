using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICountSwipe : UISwipe
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        return true;
    }

    protected override void OnClickAfterButton()
    {
        
        RefreshUI();
    }

    protected override void OnClickBeforeButton()
    {
        
        RefreshUI();
    }

    protected override void RefreshUI()
    {
        
    }
}
