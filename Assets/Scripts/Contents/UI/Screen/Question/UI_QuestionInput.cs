using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_QuestionInput : UIScreen
{
    private enum Texts
    {
        NameText
    }

    private enum InputFields
    {
        InputField
    }

    private enum Buttons
    {
        WriteButton
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        Bind<TMP_InputField>(typeof(InputFields));

        GetButton((int)Buttons.WriteButton).onClick.AddListener(OnClickWriteButton);
        return true;
    }

    protected override bool EnterWindow()
    {
        var input = Get<TMP_InputField>((int)InputFields.InputField);
        input.text = string.Empty;
        input.onSelect.AddListener((text) =>
        {
            Managers.Sound.PlaySFX("Click");
        });
        var currUserName = Managers.Game.GetCurrentPlayer().userName;
        GetText((int)Texts.NameText).SetText(currUserName);

        return true;
    }

    private void OnClickWriteButton()
    {
        Managers.Sound.PlaySFX("Click");
        // 다음 화면으로 이동
        if (SaveQuestion() == false)
            return;
        OnNextScreen<UI_PlayerSelectUI>();
    }

    private bool SaveQuestion()
    {
        var inputText = Get<TMP_InputField>((int)InputFields.InputField).text;
        if (inputText == string.Empty)
            return false;
        
        Managers.Game.CreateQuestionLog(new QuestionLog(
            Managers.Game.GetCurrentPlayer().userName,
            inputText
            ));
        return true;
    }
}