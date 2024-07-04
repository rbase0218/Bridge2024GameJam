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
        CountSwipe,
        SpySwipe,
        CategorySwipe
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
        Managers.UI.CloseWindow();

        var nameSelect = Managers.UI.ShowWindow<UINameSelect>();
        var personIndex = (Get<UISwipe>((int)Swipes.CountSwipe) as UICountSwipe).GetCount();
        var resultPersonCount = Managers.Data.personCountArray[personIndex];
        
        nameSelect.MakeChildren(resultPersonCount);
    }
}
