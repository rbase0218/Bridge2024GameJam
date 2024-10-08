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
        GetText((int)Texts.FirstText).SetText("모든 참여자들의\n암살자 지목 투표가\n종료되었습니다.");
        GetText((int)Texts.SecondText).SetText("이번 라운드\n결과를 확인합니다..");
        
        // 여기서 암살자가 인질을 모두 잡았을 경우 게임 종료 분기 필요
        if (Managers.Game._hostageList.Count == Managers.Game._userList.Count)
        {
            GetText((int)Texts.FirstText).SetText("암살자가\n 모든 인질을\n 잡았습니다.");
            GetText((int)Texts.SecondText).SetText("게임이 종료되었습니다.");
            Managers.Game.voteUser = Managers.Game._userList.Find(x => x.jobType == EJobType.Assassin);
            var uiLastChanceResult = BindNextScreen<UI_LastChanceResult>();
            uiLastChanceResult.SetInfo(true);
            return true;
        }

        // + (2024-08-22) voteUser가 Null 일 경우 임시 예외처리
        var isDuple = Managers.Game.IsDupleVoteUser();
        if (isDuple)
        {
            GetText((int)Texts.SecondText).SetText("동표가 나왔으므로\n 투표를 다시 시작합니다.");
            Managers.Game._voteList.Clear();
            Managers.Game.UndoHostage();
            
            BindNextScreen<UI_Introduce>();
            
            return true;
        }
        
        UserInfo voteUser = Managers.Game.GetVoteUser();
        if (voteUser == null)
            voteUser = Managers.Game.currentUser;
        
        Managers.Game.voteUser = voteUser;
        Managers.Game._voteList.Clear();
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_VoteResult>();
        
        return true;
    }
}