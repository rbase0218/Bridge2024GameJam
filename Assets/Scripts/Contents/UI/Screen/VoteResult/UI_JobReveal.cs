using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JobReveal : UIScreen
{
    private enum Texts
    {
        FirstText,
        NameText,
        ThirdText
    }

    private enum Buttons
    {
        NextButton
    }

    private enum Images
    {
        Picture
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var voteUser = Managers.Game.voteUser;
        var voteUserName = voteUser.userName;
        var voteUserPicture = Managers.Data.GetFrameSprite(voteUser.jobType);
        
        GetText((int)Texts.FirstText).SetText($"{voteUserName}은(는)");
        GetText((int)Texts.ThirdText).SetText("입니다.");
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_LastChance>();
        
        return true;
    }
}