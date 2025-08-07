using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VoteResult2 : UIScreen
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
        var votePlayer = Managers.Game.GetLastHostage();
        var votePlayerData = Managers.Game.FindPlayer(votePlayer.userName);

        var voteUserPicture = Managers.Data.GetFrameSprite(votePlayerData.jobType);
        var voteUserJobFrame = Managers.Data.GetFrameBGSprite(votePlayerData.jobType);

        GetText((int)Texts.FirstText).SetText($"{votePlayer.userName}은");

        var infoTexts = Managers.Data.jobInfoTexts[votePlayerData.jobType];
        // Info Text 추가
        GetText((int)Texts.SecondText).SetText(infoTexts.Item2);
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

        string voteUser = Managers.Game.GetLastHostage().userName;
        var votePlayerData = Managers.Game.FindPlayer(voteUser);

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
                    var game = Managers.Game;
                    game.CleanTurn();
                    game.ClearVoteCount();
                    game.ClearYesNoChoices();
                    game.SetContext(PlayersDataContext.DataContextType.Questioner);
                    game.QuestionManager.ClearQuestionLog();
                    Managers.Game.NextRound(); // 라운드 증가
                    if (voteUser == Managers.Game.GetCurrentHostage().userName)
                    {
                        // 만약에 이전에 암살자가 잡은 인질이 이번 투표에서 죽었다면, 랜덤으로 인질을 잡는다.
                        Managers.Game.PickRandomHostage();
                    }

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