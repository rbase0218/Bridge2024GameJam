using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : UIWindow
{
    private enum Buttons
    {
        SaveButton,
        ExitButton
    }

    private enum Sliders
    {
        BGMSlider,
        SFXSlider
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));
        
        GetButton((int)Buttons.SaveButton).onClick.AddListener(OnClickSaveButton);
        GetButton((int)Buttons.ExitButton).onClick.AddListener(OnClickExitButton);
        
        return true;
    }

    private void OnClickSaveButton()
    {
        var bgmValue = Get<Slider>((int)Sliders.BGMSlider).value;
        var sfxValue = Get<Slider>((int)Sliders.SFXSlider).value;
        
        // SoundManager에서 처리 필요
    }

    private void OnClickExitButton()
    {
        Managers.UI.CloseWindow();
    }

    private void OnDragBGMSlider()
    {
        
    }

    private void OnDragSFXSlider()
    {
        
    }
}
