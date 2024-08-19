using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGameSetting : UIWindow
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

        return true;
    }
    protected override bool EnterWindow()
    {
        return true;
    }
    
    public void OnClickButtons(UnityAction yesButton, UnityAction noButton = null)
    {
        GetButton((int)Buttons.SaveButton).onClick.RemoveAllListeners();
        GetButton((int)Buttons.ExitButton).onClick.RemoveAllListeners();
        
        GetButton((int)Buttons.SaveButton).onClick.AddListener(yesButton);
        if (noButton == null)
        {
            GetButton((int)Buttons.ExitButton).onClick.AddListener(() =>
            {
                Managers.UI.CloseWindow();
            });
        }
        else
            GetButton((int)Buttons.ExitButton).onClick.AddListener(noButton);
    }

    public void Save()
    {
        var bgmSlider = Get<Slider>((int)Sliders.BGMSlider);
        var sfxSlider = Get<Slider>((int)Sliders.SFXSlider);
        
        Managers.Sound.SetBGMVolume(bgmSlider.value);
        Managers.Sound.SetSFXVolume(sfxSlider.value);
    }
}