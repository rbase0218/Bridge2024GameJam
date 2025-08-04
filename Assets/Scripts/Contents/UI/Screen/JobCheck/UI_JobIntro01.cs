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
        CardAnim
    }

    private enum Objects
    {
        OpenCard,
        CloseCard
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

        GetButton((int)Buttons.CardAnim).onClick.AddListener(OnClickCloseCard);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        // // ===== [ Init ] =====
        GetButton((int)Buttons.CardAnim).interactable = true;
        GetObject((int)Objects.OpenCard).SetActive(false);
        GetObject((int)Objects.CloseCard).SetActive(true);
        
        // ===== [ Data Bind ] =====
        var currentUser = Managers.Game.GetCurrentPlayer();
        
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
        GetButton((int)Buttons.CardAnim).interactable = false;
        BindNextScreen<UI_JobInteraction>();
    }
}