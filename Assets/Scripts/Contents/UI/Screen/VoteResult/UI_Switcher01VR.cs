using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher01VR : UIScreen
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        UserInfo voteUser = Managers.Game._voteList[Managers.Game._voteList.Count - 1];
        
        // + (2024-08-22) voteUser가 Null 일 경우 임시 예외처리
        if (voteUser == null)
            voteUser = Managers.Game.currentUser;
        
        int voteCount = 0;
        foreach (var vote in Managers.Game._voteList)
        {
            if(voteCount < vote.Key)
                voteCount = vote.Key;
            else if(voteCount == vote.Key)
            {
                // 동표 발생
                // 동표 나올 경우 결과 창에서 문구 띄운 후 다시 Vote 화면으로 전환
            }
        }
        
        // 여기서 암살자가 인질을 모두 잡았을 경우 게임 종료 분기 필요
        if (Managers.Game._hostageList.Count == Managers.Game._userList.Count)
        {
            Managers.Game.voteUser = Managers.Game._userList.Find(x => x.jobType == EJobType.Assassin);
            var uiLastChanceResult = BindNextScreen<UI_LastChanceResult>();
            uiLastChanceResult.SetInfo(true);
            return true;
        }
        
        Managers.Game.voteUser = voteUser;
        Managers.Game._voteList.Clear();
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_VoteResult>();
        
        return true;
    }
}