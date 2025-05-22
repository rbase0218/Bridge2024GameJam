using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        
        var bgmSlider = Get<Slider>((int)Sliders.BGMSlider);
        var sfxSlider = Get<Slider>((int)Sliders.SFXSlider);
        bgmSlider.value = Managers.Data.BGMVolume;
        sfxSlider.value = Managers.Data.SFXVolume;
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            Managers.Sound.SetBGMVolume(value);
        });
        sfxSlider.onValueChanged.AddListener((value) =>
        {
            Managers.Sound.SetSFXVolume(value);
        });

        return true;
    }
    protected override bool EnterWindow()
    {
        var bgmSlider = Get<Slider>((int)Sliders.BGMSlider);
        var sfxSlider = Get<Slider>((int)Sliders.SFXSlider);
        bgmSlider.value = Managers.Data.BGMVolume;
        sfxSlider.value = Managers.Data.SFXVolume;
        return true;
    }
    
    public void OnClickButtons(UnityAction yesButton, UnityAction noButton = null)
    {
        GetButton((int)Buttons.SaveButton).onClick.RemoveAllListeners();
        GetButton((int)Buttons.ExitButton).onClick.RemoveAllListeners();
        
        GetButton((int)Buttons.SaveButton).onClick.AddListener(yesButton);
        // if (noButton == null)
        // {
        //     GetButton((int)Buttons.ExitButton).onClick.AddListener(() =>
        //     {
        //         Managers.UI.CloseWindow();
        //     });
        // }
        // else
        //     GetButton((int)Buttons.ExitButton).onClick.AddListener(noButton);
    }
    
    public void Save()
    {
        var bgmSlider = Get<Slider>((int)Sliders.BGMSlider);
        var sfxSlider = Get<Slider>((int)Sliders.SFXSlider);
        
        Managers.Sound.SetBGMVolume(bgmSlider.value);
        Managers.Sound.SetSFXVolume(sfxSlider.value);
    }
}