using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JobIntro01 : UIScreen
{
    private enum Texts
    {
        NameText,
        JobNameText
    }

    private enum Images
    {
        Frame
    }

    private enum Buttons
    {
        CloseCard
    }

    private enum Objects
    {
        CloseCard,
        JobFrame
    }
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        // Bind or Event Bind 
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));
        
        GetButton((int)Buttons.CloseCard).onClick.AddListener(OnClickCloseCard);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        // ===== [ Init ] =====
        GetObject((int)Objects.CloseCard).SetActive(true);
        GetObject((int)Objects.JobFrame).SetActive(false);
        
        // ===== [ Data Bind ] =====
        var currentUser = Managers.Game.currentUser;
        
        GetText((int)Texts.NameText).SetText(currentUser.userName);
        GetText((int)Texts.JobNameText).SetText(Managers.Data.GetJobText(currentUser.jobType));
        GetImage((int)Images.Frame).sprite = Managers.Data.GetFrameSprite(currentUser.jobType);
        
        
        // 다음 Screen 연결하기
        if (UseAutoNextScreen)
            BindNextScreen<UI_JobInteraction>();
        
        return true;
    }

    private void OnClickCloseCard()
    {
        Managers.Sound.PlaySFX("Card");
        GetObject((int)Objects.CloseCard).SetActive(false);
        GetObject((int)Objects.JobFrame).SetActive(true);
        BindNextScreen<UI_JobInteraction>();
    }
}