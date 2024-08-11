using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSelector : UIBase
{
    private enum Texts
    {
        InfoText
    }

    private enum Buttons
    {
        SubmitButton
    }
    
    public void Binding()
    {
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
    }
    
}
