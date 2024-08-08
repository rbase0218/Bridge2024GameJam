using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextSegment : UIBase
{
    public enum Texts
    {
        Text
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        
        return true;
    }
    
    public void SetInfo(string text, float fontSize)
    {
        SetText(text);
        SetFontSize(fontSize);
    }

    public void SetFontSize(float fontSize)
    {
        GetText((int)Texts.Text).fontSize = fontSize;
    }

    public void SetText(string text)
    {
        GetText((int)Texts.Text).text = text;
    }
}
