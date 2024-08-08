using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextSegment : UIBase
{
    private TMP_Text _text;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        _text = Utils.TryOrAddComponent<TMP_Text>(gameObject);
        
        return true;
    }
    
    public void SetFontSize(float fontSize)
    {
        _text.fontSize = fontSize;
    }

    public void SetFontColor(Color color)
    {
        _text.color = color;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
