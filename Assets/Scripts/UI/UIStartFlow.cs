using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartFlow : UIWindow
{
    private enum Buttons
    {
        NextButton,
        ExitButton
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        return true;
    }
}
