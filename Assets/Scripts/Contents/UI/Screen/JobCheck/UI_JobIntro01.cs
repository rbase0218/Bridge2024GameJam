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
        // Component에 데이터를 입력하는 곳
        var user = Managers.Game._currentUser;
        
        // 현재 유저의 이름 데이터 등록
        GetText((int)Texts.NameText).SetText(user.userName);
        
        // 현재 유저의 직업 데이터 등록
        GetImage((int)Images.Frame).sprite = Managers.Data.GetFrameSprite(user.jobType);
        
        // 유저가 일정 시간 동안 카드를 열지 않는다면?
        bool test = true;
        _gauge.onGaugeTimer += (x) =>
        {
            if ((1 - x) < 0.5f && test)
            {
                test = false;
                
                OnClickCloseCard();
            }
        };
        
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
