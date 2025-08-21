using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Switcher01VR_Last : UIScreen
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
        GetText((int)Texts.SecondText).SetText("최종 투표\n결과를 확인합니다..");

        var voteData = Managers.Game.GetMostChosenAnswer();
        if (voteData == "동점")
        {
            GetText((int)Texts.SecondText).SetText("동표가 나왔으므로\n 투표를 다시 시작합니다.");

            // 암살자도 의심 안받게 인질 다시 잡아야함.
            // 재토론 이후, 중간에 인질 선택하고 싶은 대상이 바뀌는 경우도 있으니까. 재투표시 지목 대상 바뀌는 것 허용.
            Managers.Game.CleanTurn();
            Managers.Game.ClearVoteCount();
            Managers.Game.ClearYesNoChoices();

            BindNextScreen<UI_Switcher01_Last2>();
            return true;
        }
        else if (voteData == "아니오")
        {
            // 반대표 우세: 발의자 역할 공개 화면으로 이동
            BindNextScreen<UI_VoteResult2_Last>();
            return true;
        }

        if (UseAutoNextScreen)
            BindNextScreen<UI_VoteResult_Last>();

        return true;
    }
}