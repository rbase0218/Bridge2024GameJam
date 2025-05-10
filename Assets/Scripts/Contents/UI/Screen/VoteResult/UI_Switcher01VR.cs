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

        // 여기서 암살자가 인질을 모두 잡았을 경우 게임 종료 분기 필요
        // 인질로 잡힌 사람 수 (죽은 사람 포함)
        // 현재 인질 리스트는 투표 즉시 바로 카운트 + 1. 첫 턴에는 인질 리스트 카운트 2로 시작.
        // 그러므로 게임은 전체인원 -1 턴으로 진행됨. 1:1 상황에서는 암살자가 자동 승리. -> 전체 인원 수 - 1 해줘야 함.

        /*
        암살자는 자기 포함 인질로 잡을 수 있고, 전체 인원 수 - 1번 잡으면 승리로 변경. ex) 4인 시 3턴, 5인시 4턴 진행.
        왜냐? 1:1 상황이 되면 무의미한 턴이 진행.

        1:1 상황이 되었는데 남은 귀빈이 그 라운드에 인질이 된 경우. -> 암살자 혼자 떠드는 상황이 됨.
        남은 귀빈 한 명 입장에서 나는 마피아가 아니니까? 무조건 암살자를 지목하게 됨. 이 시점부터는 블러핑 불가.

        5인 부터는 광대가 있기 때문에 1:1은 무조건 암살자와 광대가 하게 됨.
        1:1 광대 마피아 상황이 되었을 때. 광대에게 압도적으로 유리. 하던 대로, 마피아인 척하면 되니까

        게임이 빨리 끝나는 버그 방지하기 위해 죽은 사람은 인질로 치지 않는 룰 롤백. 처음처럼 죽은 사람도 포함해서 인질로 잡아야 함.
        죽은 사람도 투표 가능 유지. -> 어차피 암살자는 투표를 하지 않기 때문에, 시민(+광대) 끼리 투표. 이 기표자 수가 적어지면 암살자에게 절대적으로 유리.
         */

        var voteData = Managers.Game.GetMaxVotePlayerName();
        Managers.Game.ClearVoteCount();
        
        // + (2024-08-22) voteUser가 Null 일 경우 임시 예외처리
        if(voteData.Count == 0 || voteData.Count == 2)
        {
            GetText((int)Texts.SecondText).SetText("동표가 나왔으므로\n 토론과 투표를 다시 시작합니다.");
        
            // 암살자도 의심 안받게 인질 다시 잡아야함. 재토론 이후, 중간에 인질 선택하고 싶은 대상이 바뀌는 경우도 있으니까. 재투표시 지목 대상 바뀌는 것 허용.
            Managers.Game.UndoHostage();
        
            BindNextScreen<UI_Switcher02>();
        
            return true;
        }
        
        if (UseAutoNextScreen)
            BindNextScreen<UI_VoteResult>();

        return true;
    }
}