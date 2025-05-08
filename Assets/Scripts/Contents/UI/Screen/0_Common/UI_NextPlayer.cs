using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NextPlayer : UIScreen
{
    private bool isNext = false;
    
    private enum Texts
    {
        NameA,
        NameB
    }

    private enum Buttons
    {
        NextButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }

    protected override bool EnterWindow()
    {
        isNext = false;
        
        GetText((int)Texts.NameA).SetText(Managers.Game.GetCurrentPlayer().userName);
        
        // 유저 리스트를 가져온다.
        // 리스트의 끝 인덱스에 도달했는지 확인한다.
        if (!Managers.Game.IsLastPlayer())
        {
            isNext = true;
            GetText((int)Texts.NameB).SetText(Managers.Game.GetNextPlayer().userName);
            if (UseAutoNextScreen)
                BindNextScreen<UI_JobIntro01>();
        }
        else
        {
            isNext = false;
            GetText((int)Texts.NameB).SetText("종료");
            GetText((int)Texts.NameB).faceColor = Color.red;
            if (UseAutoNextScreen)
                BindNextScreen<UI_Switcher01>();
        }

        return true;
    }
    
    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");

        if (isNext)
        {
            OnNextScreen<UI_JobIntro01>();
        }
        else
        {
            OnNextScreen<UI_Switcher01>();
        }
    }
}