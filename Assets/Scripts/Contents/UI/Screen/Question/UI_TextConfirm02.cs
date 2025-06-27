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
        YesButton,
        NoButton
    }

    private bool isPass = false;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        Bind<TMP_InputField>(typeof(InputFields));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.YesButton).onClick.AddListener(OnClickYesButton);
        GetButton((int)Buttons.NoButton).onClick.AddListener(OnClickNoButton);

        return true;
    }
    
    protected override bool EnterWindow()
    {
        isPass = false;
        
        var log = Managers.Game.GetRandQuestion();
        Debug.Log(log.question);
        if (log.question == null)
        {
            OnNextScreen<UI_Switcher02>();
            return false;
        }

        string answererName = log.answerer;
        string questionText = log.question;
        
        GetText((int)Texts.Text).text = answererName;
        Get<TMP_InputField>((int)InputFields.InputField).text = questionText;

        return true;
    }

    private void OnClickYesButton()
    {
        Managers.Sound.PlaySFX("Click");
        CheckForNextScreenMove();
    }

    private void OnClickNoButton()
    {
        Managers.Sound.PlaySFX("Click");
        CheckForNextScreenMove();
    }

    private void CheckForNextScreenMove()
    {
        if (isPass)
            OnNextScreen<UI_Switcher02>();
        else
            OnNextScreen<UI_TextConfirm01>();
    }
}
