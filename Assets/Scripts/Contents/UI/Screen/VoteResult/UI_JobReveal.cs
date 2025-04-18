using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        var nextButton = GetButton((int)Buttons.NextButton);
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(OnClickNextButton);

        return true;
    }

    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");

        var voteUser = Managers.Game.voteUser;
        var isAssasinWin = Managers.Game._hostageList.Count >= Managers.Game._userList.Count - 1;
        Debug.Log($"투표: {voteUser.jobType}, isAssasinWin: {isAssasinWin}");
        switch (voteUser.jobType)
        {
            case EJobType.VIP:
                voteUser.isDie = true;
                // 게임 계속 진행
                
                if (isAssasinWin)
                {
                    Debug.Log("Assassin Win");
                    Managers.Game.voteUser = Managers.Game._userList.Find(x => x.jobType == EJobType.Assassin);
                    OnNextScreen<UI_LastChanceResult>().SetInfo(true);
                }
                else
                {
                    Managers.Sound.SetBGMVolume(Managers.Data.BGMVolume);
                    OnNextScreen<UI_Sequence02>();
                }
                break;
            case EJobType.Actor:
            case EJobType.Assassin:
                OnNextScreen<UI_LastChance>();
                break;
        }
    }
}