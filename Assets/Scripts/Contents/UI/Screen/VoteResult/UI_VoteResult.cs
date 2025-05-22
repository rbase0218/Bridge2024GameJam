using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VoteResult : UIScreen
{
    private enum Texts
    {
        SecondText
    }
    
    private enum Buttons
    {
        CloseCard
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        Managers.Sound.SetBGMVolumeNoneSave(0);
        var voteUserName = Managers.Game.GetMaxVotePlayerName()[0];
        
        GetText((int)Texts.SecondText).SetText(
            $"{voteUserName}님이 암살자로\n지목되었습니다.");
        GetButton((int)Buttons.CloseCard).gameObject.SetActive(true);
        GetButton((int)Buttons.CloseCard).onClick.AddListener(OnClickOpenCardButton);

        if(UseAutoNextScreen)
            BindNextScreen<UI_JobReveal>();
        return true;
    }
    
    private void OnClickOpenCardButton()
    {
        Managers.Sound.PlaySFX("Card");
        GetButton((int)Buttons.CloseCard).gameObject.SetActive(false);
        GetButton((int)Buttons.CloseCard).onClick.RemoveAllListeners();
        
        OnNextScreen<UI_JobReveal>();
    }
}