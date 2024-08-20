using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JobReveal : UIScreen
{
    private enum Texts
    {
        FirstText,
        JobText,
        ThirdText,
        ButtonText
    }

    private enum Buttons
    {
        NextButton
    }

    private enum Images
    {
        Picture,
        BG
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
        var voteUserJobFrame = Managers.Data.GetFrameBGSprite(voteUser.jobType);
        
        GetText((int)Texts.FirstText).SetText($"{voteUserName}은(는)");

        var infoTexts = Managers.Data.jobInfoTexts[voteUser.jobType];
        // Info Text 추가
        GetText((int)Texts.ThirdText).SetText(infoTexts.Item2);
        // Button Text 변경
        GetText((int)Texts.ButtonText).SetText(infoTexts.Item1);

        // 초상화 세팅
        GetImage((int)Images.Picture).sprite = voteUserPicture;
        GetImage((int)Images.BG).sprite = voteUserJobFrame;
        // 직업명 추가
        GetText((int)Texts.JobText).SetText(Managers.Data.GetJobText(voteUser.jobType));
        
        // Event Bind
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }

    private void OnClickNextButton()
    {
        var voteUserJob = Managers.Game.voteUser.jobType;

        switch (voteUserJob)
        {
            case EJobType.VIP:
                Debug.Log("다음 라운드 진행");
                break;
            case EJobType.Actor:
            case EJobType.Assassin:
                OnNextScreen<UI_LastChance>();
                break;
        }
    }
}