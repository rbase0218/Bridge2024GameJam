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
        var votePlayerName = Managers.Game.GetMaxVotePlayerName()[0];
        var votePlayerData = Managers.Game.FindPlayer(votePlayerName);
        
        var voteUserPicture = Managers.Data.GetFrameSprite(votePlayerData.jobType);
        var voteUserJobFrame = Managers.Data.GetFrameBGSprite(votePlayerData.jobType);
        
        GetText((int)Texts.FirstText).SetText($"{votePlayerName}은(는)");
        
        var infoTexts = Managers.Data.jobInfoTexts[votePlayerData.jobType];
        // Info Text 추가
        GetText((int)Texts.ThirdText).SetText(infoTexts.Item2);
        // Button Text 변경
        GetText((int)Texts.ButtonText).SetText(infoTexts.Item1);
        
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

        var voteUser = Managers.Game.GetMaxVotePlayerName()[0];
        var votePlayerData = Managers.Game.FindPlayer(voteUser);
        
        // 인질 수가 자기 자신을 제외한 수와 동일하다면 암살자 승리
        // Debug.Log($"투표: {voteUser.jobType}, isAssassinWin: {isAssassinWin}");
        switch (votePlayerData.jobType)
        {
            case EJobType.VIP:
                votePlayerData.isDie = true;
                
                // 암살자가 이겼는지 확인한다.
                var isAssassinWin = Managers.Game.ValidateVictory();
                if (isAssassinWin)
                {
                    OnNextScreen<UI_LastChanceResult>().SetInfo(true);
                }
                else
                {
                    // 다시 투표 수가 초기화가 되어야 한다.
                    Managers.Game.ClearVoteCount();
                    
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