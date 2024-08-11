using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JobIntro01 : UIScreen
{
    public enum Texts
    {
        NameText,
        JobNameText
    }

    public enum Images
    {
        Frame
    }

    public enum Buttons
    {
        CloseCard
    }

    public enum Objects
    {
        CloseCard,
        JobFrame
    }
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));
        
        GetButton((int)Buttons.CloseCard).onClick.AddListener(OnClickCloseCard);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var user = Managers.Game._currentUser;
        
        // 현재 유저의 이름 데이터 등록
        GetText((int)Texts.NameText).SetText(user.userName);
        
        // 현재 유저의 직업 데이터 등록
        GetImage((int)Images.Frame).sprite = Managers.Data.GetFrameSprite(user.jobType);
        
        // 유저가 일정 시간 동안 카드를 열지 않는다면?
        // _gauge.onGaugeTimer += (x) =>
        // {
        //     Debug.Log(x);
        //     if ((1 - x) < 0.8f)
        //     {
        //         OnClickCloseCard();
        //         GetButton((int)Buttons.CloseCard).onClick.RemoveAllListeners();
        //     }
        // };
        
        // 다음 Screen 연결하기
        if (UseAutoNextScreen)
            BindNextScreen<UI_JobInteraction>();
        
        return true;
    }

    private void OnClickCloseCard()
    {
        GetObject((int)Objects.CloseCard).SetActive(false);
        GetObject((int)Objects.JobFrame).SetActive(true);
    }
}
