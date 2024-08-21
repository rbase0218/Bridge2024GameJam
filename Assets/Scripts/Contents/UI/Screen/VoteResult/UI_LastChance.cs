using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_LastChance : UIScreen
{
    private enum InputFields
    {
        InputField
    }

    private enum Buttons
    {
        WriteButton
    }

    private enum Images
    {
        BG
    }
    
    private string _inputText;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<TMP_InputField>(typeof(InputFields));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        
        GetButton((int)Buttons.WriteButton).onClick.AddListener(OnClickWriteButton);
        Get<TMP_InputField>((int)InputFields.InputField).onValueChanged.AddListener(OnValueChangedInputField);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        Get<TMP_InputField>((int)InputFields.InputField).text = string.Empty;
        var voteUser = Managers.Game.voteUser;
        var voteUserJobFrame = Managers.Data.GetFrameBGSprite(voteUser.jobType);
        GetImage((int)Images.BG).sprite = voteUserJobFrame;
        
        return true;
    }

    private void OnClickWriteButton()
    {
        var writeText = Get<TMP_InputField>((int)InputFields.InputField).text;
        OnNextScreen<UI_LastChanceResult>().SetInfo(writeText == Managers.Game.gameTopic);
    }

    private void OnValueChangedInputField(string text)
    {
        _inputText = text;
    }
}