using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManual : UIWindow
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;


        return true;
    }
    protected override bool EnterWindow()
    {
        return true;
    }
}