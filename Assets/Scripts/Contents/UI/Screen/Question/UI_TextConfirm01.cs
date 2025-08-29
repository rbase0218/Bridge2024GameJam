using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TextConfirm01 : UIScreen
{
    private enum Buttons
    {
        CardAnim,
        YesButton,
        NoButton
    }

    private enum Texts
    {
        Text,
        WordText
    }

    private enum Objects
    {
        OpenCard,
        CloseCard
    }

    private bool hasNextQuestion = false;
    private QuestionLog _questionLog;

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        // 질문자 이름 호출
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        GetButton((int)Buttons.CardAnim).onClick.AddListener(OnClickCloseCard);

        return true;
    }

    protected override bool EnterWindow()
    {
        GetButton((int)Buttons.CardAnim).interactable = true;
        GetButton((int)Buttons.YesButton).interactable = false;
        GetButton((int)Buttons.NoButton).interactable = false;

        GetText((int)Texts.WordText).gameObject.SetActive(false);
        GetObject((int)Objects.OpenCard).SetActive(false);
        GetObject((int)Objects.CloseCard).SetActive(true);

        // 퀘스트 정보를 가져온다.
        _questionLog = Managers.Game.QuestionManager.GetRandomQuestionLog();

        string hostageName = Managers.Game.GetCurrentHostage().userName;
        string text = _questionLog.question;

        GetText((int)Texts.Text).SetText(hostageName);
        GetText((int)Texts.WordText).SetText(text);
        
        GetButton((int)Buttons.YesButton).onClick.RemoveAllListeners();
        GetButton((int)Buttons.NoButton).onClick.RemoveAllListeners();

        GetButton((int)Buttons.YesButton).onClick.AddListener(OnClickYesButton);
        GetButton((int)Buttons.NoButton).onClick.AddListener(OnClickNoButton);

        return true;
    }

    private void OnClickCloseCard()
    {
        Managers.Sound.PlaySFX("Card");
        GetButton((int)Buttons.CardAnim).interactable = false;
        StartCoroutine(CardOpenDelay());
    }

    private IEnumerator CardOpenDelay()
    {
        yield return new WaitForSeconds(0.1f);
        GetText((int)Texts.WordText).gameObject.SetActive(true);
        GetButton((int)Buttons.YesButton).interactable = true;
        GetButton((int)Buttons.NoButton).interactable = true;
    }

    private void OnClickYesButton()
    {
        Managers.Sound.PlaySFX("Click");
        
        // 답변 저장 - QuestionLogManager에 직접 업데이트
        Managers.Game.QuestionManager.SetCurrentQuestionAnswer("예");
        CheckForNextScreenMove();
    }

    private void OnClickNoButton()
    {
        Managers.Sound.PlaySFX("Click");
        
        // 답변 저장 - QuestionLogManager에 직접 업데이트
        Managers.Game.QuestionManager.SetCurrentQuestionAnswer("아니오");
        CheckForNextScreenMove();
    }

    private void CheckForNextScreenMove()
    {
        hasNextQuestion = Managers.Game.QuestionManager.NextQuestion();

        if (hasNextQuestion == false)
            OnNextScreen<UI_Switcher02>();
        else
            OnNextScreen<UI_TextConfirm01>();
    }
}
