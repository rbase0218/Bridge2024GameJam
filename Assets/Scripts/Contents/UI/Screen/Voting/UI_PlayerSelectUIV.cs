using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSelectUIV : UIScreen
{
    private enum Objects
    {
        Board_A,
        Board_B
    }

    private enum Buttons
    {
        SubmitButton_A,
        SubmitButton_B
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.SubmitButton_A).onClick.AddListener(OnClickSubmitButtonA);
        GetButton((int)Buttons.SubmitButton_B).onClick.AddListener(OnClickSubmitButtonB);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        // 직업에 따라서 다른 Board를 선택한다.
        // 현재 Question의 타입은?
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_ClockSwitcherV>();
        
        return true;
    }

    private void OnClickSubmitButtonA()
    {
        
    }

    private void OnClickSubmitButtonB()
    {
        
    }
}