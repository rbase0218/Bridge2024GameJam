using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManual : UIWindow
{
    private enum Buttons
    {
        BeforeButton,
        AfterButton,
        CloseButton
    }

    private enum Objects
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh
    }

    private int index;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));
        
        GetButton((int)Buttons.BeforeButton).onClick.AddListener(OnClickBeforeButton);
        GetButton((int)Buttons.AfterButton).onClick.AddListener(OnClickAfterButton);
        GetButton((int)Buttons.CloseButton).onClick.AddListener(() =>
        {
            Managers.Sound.PlaySFX("Click");
            Managers.UI.CloseWindow();
        });
        
        return true;
    }
    protected override bool EnterWindow()
    {
        return true;
    }
    
    private void OnClickBeforeButton()
    {
        Managers.Sound.PlaySFX("Click");

        if (index == 0)
            return;
        
        GetObject((int)Objects.First + index).SetActive(false);
        GetObject((int)Objects.First + --index).SetActive(true);
    }
    
    private void OnClickAfterButton()
    {
        Managers.Sound.PlaySFX("Click");

        if (index == 6)
            return;
        
        GetObject((int)Objects.First + index).SetActive(false);
        GetObject((int)Objects.First + ++index).SetActive(true);
    }
}