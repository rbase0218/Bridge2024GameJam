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
        Get<TMP_InputField>((int)InputFields.InputField).text = " ";
        GetText((int)Texts.NameText).text = Managers.Game.currentUser.userName;
        onSceneChanged?.AddListener(SaveQuestion);
        return true;
    }

    private void OnClickWriteButton()
    {
        // 다음 화면으로 이동
        SaveQuestion();
        OnNextScreen<UI_PlayerSelectUI>();
    }

    private void SaveQuestion()
    {
        var inputText = Get<TMP_InputField>((int)InputFields.InputField).text;
        
        // 질문 Question 등록
        Managers.Game.SetQuestion(inputText);
    }
}
