using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher01VR : UIScreen
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        UserInfo voteUser = Managers.Game._voteList[Managers.Game._voteList.Count - 1];
        Managers.Game.voteUser = voteUser;
        Managers.Game._voteList.Clear();
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_VoteResult>();
        
        return true;
    }
}