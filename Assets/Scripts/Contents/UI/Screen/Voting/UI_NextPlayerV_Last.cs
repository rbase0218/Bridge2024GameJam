using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NextPlayerV_Last : UIScreen
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
        GetText((int)Texts.NameA).SetText(currentPlayerName);
        
        // 유저 리스트를 가져온다.
        // 리스트의 끝 인덱스에 도달했는지 확인한다.
        if (!Managers.Game.IsLastPlayer())
        {
            isNext = true;
            
            GetText((int)Texts.NameB).faceColor = Color.white;
            GetText((int)Texts.NameB).SetText(Managers.Game.GetNextPlayer().userName);
            
            if (UseAutoNextScreen)
                BindNextScreen<UI_PlayerSelectUIV_Last>();
        }
        else
        {
            isNext = false;
            
            GetText((int)Texts.NameB).SetText("종료");
            GetText((int)Texts.NameB).faceColor = Color.red;
            
            // if (UseAutoNextScreen)
            //     BindNextScreen<UI_Switcher01VR>();
        }
        
        Managers.Game.UpdateNextPlayer();
        return true;
    }
    
    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");

        if (isNext)
            OnNextScreen<UI_PlayerSelectUIV_Last>();
        // else
        //     OnNextScreen<UI_Switcher01VR>();
        //TODO: 최후 투표 지목자 확인
    }
}