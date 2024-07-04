using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartFlow : UIWindow
{
    private enum Buttons
    {
        NextButton,
        //ExitButton
    }

    private enum Swipes
    {
        CountSwipe
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        Bind<UISwipe>(typeof(Swipes));
        
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }

    private void OnClickNextButton()
    {
        Debug.Log("Click - Next Button");
        
        // Current UI를 닫는다.
        Managers.UI.CloseWindow();
        // NameSelect 창을 열고 Instance를 보유한다.
        var nameSelect = Managers.UI.ShowWindow<UINameSelect>();
        var stringCount = Get<UISwipe>((int)Swipes.CountSwipe).GetData();
        var personCount = Convert.ToInt32(stringCount);
        
        // NameSelect 창에서 MakeChildren을 통해서 Child를 생성한다.
        nameSelect.ShowChildren(personCount);

    }
}
