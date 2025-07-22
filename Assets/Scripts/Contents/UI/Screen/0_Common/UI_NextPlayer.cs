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

        var currentPlayerName = Managers.Game.GetCurrentPlayer().userName;
        var nextPlayerName = Managers.Game.GetNextPlayer().userName;
        
        GetText((int)Texts.NameA).SetText(currentPlayerName);

        // 현재 유저가 마지막 유저인지 검사한다.
        if (!Managers.Game.IsLastPlayer())
        {
            isNext = true;
            
            GetText((int)Texts.NameB).SetText(nextPlayerName);
            Managers.Game.UpdateNextPlayer();
            
            if (UseAutoNextScreen)
                BindNextScreen<UI_JobIntro01>();
        }
        else
        {
            isNext = false;
            
            GetText((int)Texts.NameB).SetText("종료");
            GetText((int)Texts.NameB).faceColor = Color.red;

            if (UseAutoNextScreen)
            {
                Managers.Game.CleanTurn();
                BindNextScreen<UI_Switcher01>();
            }
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
            Managers.Game.CleanTurn();
            OnNextScreen<UI_Switcher01>();
        }
    }
}