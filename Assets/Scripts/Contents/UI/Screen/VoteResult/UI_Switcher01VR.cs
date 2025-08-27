using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Switcher01VR : UIScreen
{
    private enum Texts
    {
        FirstText,
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
        GetText((int)Texts.FirstText).SetText("모든 참여자들의\n투표가\n종료되었습니다.");
        GetText((int)Texts.SecondText).SetText("이번 라운드\n결과를 확인합니다..");

        var inGameSetting = Managers.UI.GetWindow<UI_InGameSetting>();
        if (inGameSetting != null)
        {
            inGameSetting.HideInfoButton();
        }

        // 투표 완료 후 임시 인질을 실제 인질로 확정
        Managers.Game.ConfirmTemporaryHostage();

        var voteData = Managers.Game.GetMaxVotePlayerName();

        // + (2024-08-22) voteUser가 Null 일 경우 임시 예외처리
        if (Managers.Game.GetMostChosenAnswer() == "동점")
        {
            GetText((int)Texts.SecondText).SetText("동표가 나왔으므로\n 토론과 투표를 다시 시작합니다.");

            // 암살자도 의심 안받게 인질 다시 잡아야함.
            // 재토론 이후, 중간에 인질 선택하고 싶은 대상이 바뀌는 경우도 있으니까. 재투표시 지목 대상 바뀌는 것 허용.
            Managers.Game.CleanTurn();
            Managers.Game.UndoHostage();
            Managers.Game.CancelTemporaryHostage(); // 임시 인질 취소
            Managers.Game.ClearVoteCount();
            Managers.Game.ClearYesNoChoices();

            BindNextScreen<UI_Switcher02>();

            return true;
        }
        else if (Managers.Game.GetMostChosenAnswer() == "아니오")
        {

            GetText((int)Texts.SecondText).SetText("반대가 더 많이 나왔으므로\n 공개 없이 다음 라운드를 진행합니다.");

            Managers.Game.CleanTurn();
            Managers.Game.ClearVoteCount();
            Managers.Game.ClearYesNoChoices();
            // 유저 데이터 설정 => Questioner
            Managers.Game.SetContext(PlayersDataContext.DataContextType.Questioner);

            // 퀘스트 목록 클리어.
            Managers.Game.QuestionManager.ClearQuestionLog();
            Managers.Game.NextRound(); // 라운드 증가

            BindNextScreen<UI_Sequence02>();

            return true;
        }

        if (UseAutoNextScreen)
            BindNextScreen<UI_VoteResult>();

        return true;
    }
}