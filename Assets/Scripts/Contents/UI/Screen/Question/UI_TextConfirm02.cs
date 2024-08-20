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
        GetText((int)Texts.Text).text = Managers.Game.selectUserName;
        Get<TMP_InputField>((int)InputFields.InputField).text = Managers.Game.questionText;

        if (UseAutoNextScreen)
        {
            BindNextScreen<UI_NextPlayerQ>();
        }

        return true;
    }

    private void OnClickYesButton()
    {
        OnNextScreen<UI_NextPlayerQ>();
    }

    private void OnClickNoButton()
    {
        OnNextScreen<UI_NextPlayerQ>();
    }
}
