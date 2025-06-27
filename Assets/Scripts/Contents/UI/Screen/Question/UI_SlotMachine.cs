using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotMachine : UIScreen
{
    private enum Buttons
    {
        NextButton
    }
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        return true;
    }

    protected override bool EnterWindow()
    {
        var maxCount = Managers.Game.GetQuestionLogCount();
        var randNum = Random.Range(1, maxCount);
        
        // 랜덤 수를 미리 정함.
        Managers.Game.SetRandQuestionCount(randNum);
        Debug.Log("다음 질문 개수 : " + randNum);
        
        // 이후로 SlotMachine을 만들면 됨.
        
        return true;
    }

    private void OnClickNextButton()
    {
        OnNextScreen<UI_TextConfirm01>();
    }
}
