using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_IntroStage : UIStage
{
    public enum Screens
    {
        Intro01
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<UIScreen>(typeof(Screens));

        return true;
    }
    
    public override void OnFirstScreen()
    {
        
    }
}
