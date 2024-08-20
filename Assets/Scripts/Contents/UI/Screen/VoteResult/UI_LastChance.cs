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
    
    private string _inputText;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<TMP_InputField>(typeof(InputFields));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.WriteButton).onClick.AddListener(OnClickWriteButton);
        Get<TMP_InputField>((int)InputFields.InputField).onValueChanged.AddListener(OnValueChangedInputField);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        Get<TMP_InputField>((int)InputFields.InputField).text = string.Empty;
        
        return true;
    }

    private void OnClickWriteButton()
    {
        OnNextScreen<UI_LastChanceResult>().SetInfo(false);
    }

    private void OnValueChangedInputField(string text)
    {
        _inputText = text;
    }
}