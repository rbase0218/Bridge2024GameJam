using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINameTagContainer : UIBase
{
    public enum Texts
    {
        Text
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        Bind<UITextSegment>(typeof(Texts));
        
        return true;
    }

    public void SetText(string text)
    {
        Get<UITextSegment>((int)Texts.Text).SetText(text);
    }
}
