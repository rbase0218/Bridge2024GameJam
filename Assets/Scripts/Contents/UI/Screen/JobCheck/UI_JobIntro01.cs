using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JobIntro01 : UIScreen
{
    public enum Texts
    {
        NameText,
        JobNameText
    }

    public enum Images
    {
        Frame
    }
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var sprite = Managers.Data.ActorSprite;
        GetImage((int)Images.Frame).sprite = sprite;
        // 데이터 연결 필요
        return true;
    }
}
