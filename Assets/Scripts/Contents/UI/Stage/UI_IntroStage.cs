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
    
    public override void OpenScreen(UI_Gauge gauge)
    {
        var intro = Managers.UI.GetWindow<UI_Intro01>();
        intro.SetInfo(gauge);
        
        Managers.UI.ShowWindow<UI_Intro01>();
    }
}
