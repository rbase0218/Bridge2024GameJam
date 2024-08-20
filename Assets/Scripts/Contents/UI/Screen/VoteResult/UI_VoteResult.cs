using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VoteResult : UIScreen
{
    private enum Texts
    {
        SecondText
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindText(typeof(Texts));
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var voteUserName = Managers.Game.voteUser.userName;
        
        GetText((int)Texts.SecondText).SetText(
            $"{voteUserName}님이 암살자로\n지목되었습니다.");
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_JobReveal>();
        return true;
    }
}