using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NextPlayerQ : UIScreen
{
    private bool isNext;
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
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        GetText((int)Texts.NameA).SetText(Managers.Game.prevUser.userName);
        
        if (Managers.Game.GetQuestionUser())
        {
            isNext = true;
            GetText((int)Texts.NameB).SetText(Managers.Game.currentUser.userName);
        }
        else
        {
            isNext = false;
            GetText((int)Texts.NameB).SetText("종료");
            GetText((int)Texts.NameB).faceColor = Color.red;
        }

        return true;
    }
    
    private void OnClickNextButton()
    {
        if (isNext)
            OnNextScreen<UI_QuestionInput>();
        else
            OnNextScreen<UI_Switcher02>();
    }
}
