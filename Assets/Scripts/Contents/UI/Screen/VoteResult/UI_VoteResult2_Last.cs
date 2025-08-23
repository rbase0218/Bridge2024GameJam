using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VoteResult2_Last : UIScreen
{
    private enum Texts
    {
        FirstText,
        SecondText,
        JobText,
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
        var votePlayer = Managers.Game.GetFinalVoteTarget();
        var votePlayerData = Managers.Game.FindPlayer(votePlayer.userName);

        var voteUserPicture = Managers.Data.GetFrameSprite(votePlayerData.jobType);
        var voteUserJobFrame = Managers.Data.GetFrameBGSprite(votePlayerData.jobType);

        GetText((int)Texts.FirstText).SetText($"{votePlayer.userName}은");

        var infoTexts = Managers.Data.jobInfoTexts[votePlayerData.jobType];
        // Info Text 추가
        GetText((int)Texts.SecondText).SetText(infoTexts.Item2);

        // Button Text 변경
        //GetText((int)Texts.ButtonText).SetText(infoTexts.Item1);

        // 초상화 세팅
        GetImage((int)Images.Picture).sprite = voteUserPicture;
        GetImage((int)Images.BG).sprite = voteUserJobFrame;
        // 직업명 추가
        GetText((int)Texts.JobText).SetText(Managers.Data.GetJobText(votePlayerData.jobType));

        // Event Bind
        var nextButton = GetButton((int)Buttons.NextButton);
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(OnClickNextButton);

        return true;
    }

    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");

        string voteUser = Managers.Game.GetFinalVoteTarget().userName;
        var votePlayerData = Managers.Game.FindPlayer(voteUser);

        switch (votePlayerData.jobType)
        {
            case EJobType.VIP:
                OnNextScreen<UI_LastChanceResult>().SetInfo(true, true); // 최후 투표로 시민이 죽은 경우
                break;
            case EJobType.Actor:
            case EJobType.Assassin:
                OnNextScreen<UI_LastChance>();
                break;
        }
    }
}