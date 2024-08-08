using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISequence01 : UIWindow
{
    public enum NameTag
    {
        NameTagContainer
    }

    public enum Texts
    {
        Text1,
        Text2
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        Bind<UINameTagContainer>(typeof(NameTag));
        Bind<UITextSegment>(typeof(Texts));

        return true;
    }
    
    protected override bool EnterWindow()
    {
        // Window가 Open 되기 전에 해당 처리를 진행한다.
        
        return true;
    }

    protected override void ExitWindow()
    {
        
    }

    public void SetInfo(string name, string text1, string text2)
    {
        Get<UINameTagContainer>((int)NameTag.NameTagContainer).SetInfo(name);
        Get<UITextSegment>((int)Texts.Text1).SetText(text1);
        Get<UITextSegment>((int)Texts.Text2).SetText(text2);
    }
}
