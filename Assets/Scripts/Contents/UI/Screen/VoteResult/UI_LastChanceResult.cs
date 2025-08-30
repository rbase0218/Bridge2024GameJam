using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class UI_LastChanceResult : UIScreen
{
    private enum Texts
    {
        FirstText,
        JobText,
        SecondText,
        ButtonText
    }

    private enum Images
    {
        Frame,
        BG
    }

    private enum Buttons
    {
        NextButton
    }

    private enum Objects
    {
        Defeat,
        Victory
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);

        return true;
    }

    protected override bool EnterWindow()
    {
        return true;
    }

    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");

        OnNextScreen<UI_Switcher01F>();
    }

    public void SetInfo(bool isAnswerCorrect, bool isLastChanceVote = false)
    {
        GetObject((int)Objects.Defeat).SetActive(false);
        GetObject((int)Objects.Victory).SetActive(false);
        GetText((int)Texts.ButtonText).SetText("최종 결과 확인");

        if (isAnswerCorrect)    // 정답을 맞춘 경우
        {
            GetObject((int)Objects.Defeat).SetActive(true);
            Managers.Sound.PlaySFX("Assassin");
            // Assassin의 승리
            // 다양한 게임 분기를 위해 정답입니다 문구를 제거함.
            GetText((int)Texts.FirstText).SetText("");

            if (isLastChanceVote)
            {
                GetText((int)Texts.SecondText).SetText("최후 투표의 규칙에 따라,\n 모든 귀빈은 무도회에서 \n패배했습니다.");
                GetText((int)Texts.ButtonText).SetText("넘어가기");
            }
            else
            {
                GetText((int)Texts.SecondText).SetText("암살자가\n귀빈들과의 대결에서\n승리했습니다.");
            }

            Managers.Game.SetWinner(EJobType.Assassin);
        }
        else
        {
            GetObject((int)Objects.Victory).SetActive(true);

            UserInfo voteUser = null;

            if (Managers.Game.GetFinalVoteTarget() == null)
            {
                voteUser = Managers.Game.GetLastHostage();
            }
            else
            {
                voteUser = Managers.Game.GetFinalVoteTarget();
            }
            var voteUserJob = Managers.Game.FindPlayer(voteUser.userName).jobType;

            // 광대 승리
            if (voteUserJob == EJobType.Actor)
            {
                Managers.Sound.PlaySFX("Clown");

                GetText((int)Texts.FirstText).SetText("오답입니다!");
                GetText((int)Texts.SecondText).SetText("뜻밖의 광대가\n귀빈들과의 게임에서\n승리를 가져갑니다.");

                Managers.Game.SetWinner(EJobType.Actor);

            }
            else if (voteUserJob == EJobType.Assassin)
            {
                GetObject((int)Objects.Victory).SetActive(true);

                // 귀빈 승리
                Managers.Sound.PlaySFX("Guest");

                GetText((int)Texts.FirstText).SetText("오답입니다!");
                GetText((int)Texts.JobText).SetText("귀빈");
                GetText((int)Texts.SecondText).SetText("귀빈들이\n그들의 무도회를\n지켜냈습니다.");

                Managers.Game.SetWinner(EJobType.VIP);
            }
        }
        var winJob = Managers.Game.GetWinner();

        var winnerJobFrame = Managers.Data.GetFrameBGSprite(winJob);
        var jobText = Managers.Data.GetJobText(winJob);

        GetText((int)Texts.JobText).SetText(jobText);
        GetImage((int)Images.BG).sprite = winnerJobFrame;
        GetImage((int)Images.Frame).sprite = Managers.Data.GetFrameSprite(winJob);

        gameObject.SetActive(true);
    }
}