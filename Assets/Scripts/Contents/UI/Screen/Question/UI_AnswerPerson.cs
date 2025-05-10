using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AnswerPerson : UIScreen
{
    private enum Texts
    {
        Text
    }
    
    private enum Buttons
    {
        NextButton
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var answerUserName = Managers.Game.GetLastQuestionLog().answerer;
        GetText((int)Texts.Text).SetText(answerUserName);
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_TextConfirm01>();
        
        return true;
    }
    
    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");
        OnNextScreen<UI_TextConfirm01>();
    }
}
