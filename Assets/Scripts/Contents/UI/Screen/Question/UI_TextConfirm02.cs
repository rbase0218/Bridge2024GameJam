using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_TextConfirm02 : UIScreen
{
    private enum InputFields
    {
        InputField    
    }

    private enum Texts
    {
        Text
    }

    private enum Buttons
    {
        YesButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        Bind<TMP_InputField>(typeof(InputFields));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.YesButton).onClick.AddListener(OnClickYesButton);

        return true;
    }
    
    protected override bool EnterWindow()
    {
        var log = Managers.Game.GetLastQuestionLog();
        
        string answererName = log.answerer;
        string questionText = log.question;
        
        GetText((int)Texts.Text).text = answererName;
        Get<TMP_InputField>((int)InputFields.InputField).text = questionText;

        if (UseAutoNextScreen)
        {
            BindNextScreen<UI_NextPlayerQ>();
        }

        return true;
    }

    private void OnClickYesButton()
    {
        Managers.Sound.PlaySFX("Click");
        OnNextScreen<UI_NextPlayerQ>();
    }
}
