using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TextConfirm01 : UIScreen
{
    private enum Buttons
    {
        CloseCard
    }

    private enum Texts
    {
        Text
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        // 질문자 이름 호출
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.CloseCard).onClick.AddListener(OnClickCloseCard);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        // 랜덤 질문 가져오기
        var answerUserName = Managers.Game.QuestionManager.GetRandomQuestionLog().questioner;
        GetText((int)Texts.Text).SetText(answerUserName);
        
        if (UseAutoNextScreen)
            BindNextScreen<UI_TextConfirm02>();
        return true;
    }

    private void OnClickCloseCard()
    {
        Managers.Sound.PlaySFX("Card");
        OnNextScreen<UI_TextConfirm02>();
    }
}
