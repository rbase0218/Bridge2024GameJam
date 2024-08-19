using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NextPlayerV : UIScreen
{
    private enum Texts
    {
        NameA,
        NameB
    }

    private enum Buttons
    {
        NextButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        if(UseAutoNextScreen)
            BindNextScreen<UI_NextPlayerV>();
        
        return true;
    }
}