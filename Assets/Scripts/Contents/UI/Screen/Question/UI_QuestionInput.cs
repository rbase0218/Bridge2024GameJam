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
        onSceneChanged.AddListener(SaveQuestion);
        
        return true;
    }

    protected override bool EnterWindow()
    {
        
        return true;
    }

    private void OnClickWriteButton()
    {
        // 다음 화면으로 이동
        OnNextScreen<UI_PlayerSelectUI>();
    }

    private void SaveQuestion()
    {
        var inputText = Get<TMP_InputField>((int)InputFields.InputField).text;
        
        // 질문 Question 등록
        Managers.Game.SetQuestion(inputText);
    }
}
