using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINextOrderContainer : UIBase
{
    public enum Texts
    {
        FrontText,
        Text,
        BackText
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<UITextSegment>(typeof(Texts));

        return true;
    }
    
    public void SetInfo(string text, string frontText = "", string backText = "")
    {
        // String을 지속적으로 생성하게 되겠지만 감수할 수 있는 범위 안쪽 일듯?
        if(frontText == "") frontText = Global.NextOrderFrontText;
        if(backText == "") backText = Global.NextOrderBackText;
        
        Get<UITextSegment>((int)Texts.FrontText).SetText(frontText);
        Get<UITextSegment>((int)Texts.Text).SetText(text);
        Get<UITextSegment>((int)Texts.BackText).SetText(backText);
    }
}
