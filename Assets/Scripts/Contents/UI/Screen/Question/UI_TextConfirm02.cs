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

    // 다음 퀘스트가 존재하는가?
    private bool hasNextQuestion = false;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        Bind<TMP_InputField>(typeof(InputFields));
        
        GetButton((int)Buttons.YesButton).onClick.AddListener(OnClickYesButton);
        GetButton((int)Buttons.NoButton).onClick.AddListener(OnClickNoButton);

        return true;
    }
    
    protected override bool EnterWindow()
    {
        var question = Managers.Game.QuestionManager.GetRandomQuestionLog();
        
        string playerName = question.questioner;
        string text = question.question;
        
        // 질문 보낸 사람의 이름
        GetText((int)Texts.Text).text = playerName;
        // 질문 내용 
        Get<TMP_InputField>((int)InputFields.InputField).text = text;

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
        hasNextQuestion = Managers.Game.QuestionManager.NextQuestion();
        Debug.Log("다음 질문 존재 여부 : " + hasNextQuestion);
        
        if (hasNextQuestion == false)
            OnNextScreen<UI_Switcher02>();
        else
            OnNextScreen<UI_TextConfirm01>();
    }
}
