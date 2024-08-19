using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TextConfirm01 : UIScreen
{
    private enum Buttons
    {
        CloseCard
    }

    private enum Texts
    {
        QuestionText
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.CloseCard).onClick.AddListener(OnClickCloseCard);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        if (UseAutoNextScreen)
            BindNextScreen<UI_TextConfirm02>();
        return true;
    }

    private void OnClickCloseCard()
    {
        GetButton((int)Buttons.CloseCard).gameObject.SetActive(false);
    }
}
