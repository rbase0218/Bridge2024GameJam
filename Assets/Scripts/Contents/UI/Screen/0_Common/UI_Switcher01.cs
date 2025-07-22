using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher01 : UIScreen
{
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        if(UseAutoNextScreen)
            BindNextScreen<UI_Sequence02>();
        
        // 유저 데이터 설정 => Questioner
        Managers.Game.SetContext(PlayersDataContext.DataContextType.Questioner);
        
        // 퀘스트 목록 클리어.
        Managers.Game.QuestionManager.ClearQuestionLog();
        
        return true;
    }
}


