using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher01V : UIScreen
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        return true;
    }
    
    protected override bool EnterWindow()
    {
        if(UseAutoNextScreen)
            BindNextScreen<UI_Introduce>();
        
        // 투표는 모든 플레이어가 전부 다 보여야 한다.
        // 그렇기 때문에 VoterPicker 클래스, VoteManager 클래스를 만든다.
        return true;
    }
}