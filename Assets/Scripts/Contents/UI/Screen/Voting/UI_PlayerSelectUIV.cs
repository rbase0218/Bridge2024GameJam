using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSelectUIV : UIScreen
{
    private enum Objects
    {
        Board_A,
        Board_B
    }

    private enum Buttons
    {
        SubmitButton_A,
        SubmitButton_B
    }

    private enum Texts
    {
        FrontText,
        Text,
        BackText
    }

    private enum PlayerSelector
    {
        SelectContainerA,
        SelectContainerB
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        Bind<UIPlayerSelector>(typeof(PlayerSelector));
        
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA).onClickSubmitButton.AddListener(OnClickSubmitButtonA);
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerB).onClickSubmitButton.AddListener(OnClickSubmitButtonB);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var currentUser = Managers.Game.currentUser;
        var userList = Managers.Game._userList.FindAll((x) => x.userName != Managers.Game.currentUser.userName).Select( (x) => x.userName).ToArray();

        if (currentUser.jobType == EJobType.Assassin)
        {
            GetObject((int)Objects.Board_B).SetActive(true);
            GetObject((int)Objects.Board_A).SetActive(false);

            var selectorB = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerB);
            selectorB.ShowButton(userList);
            
            GetText((int)Texts.FrontText).SetText("당신은");
            GetText((int)Texts.Text).SetText("암살자");
            GetText((int)Texts.BackText).SetText("입니다.");
        }
        else
        {
            GetObject((int)Objects.Board_B).SetActive(false);
            GetObject((int)Objects.Board_A).SetActive(true);
            
            var selectorA = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA);
            selectorA.ShowButton(userList);
            
            GetText((int)Texts.FrontText).SetText("이번 투표 순서는");
            GetText((int)Texts.Text).SetText(currentUser.userName);
            GetText((int)Texts.BackText).SetText("입니다.");
        }
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_ClockSwitcherV>();
        
        return true;
    }

    // 광대 및 귀빈
    private void OnClickSubmitButtonA(string text)
    {
        var findUser = Managers.Game._userList.Find( x => x.userName == text);
        Managers.Game._voteList.Add(Managers.Game._voteList.Count, findUser);
    }

    // 암살자
    private void OnClickSubmitButtonB(string text)
    {
        var findUser = Managers.Game._userList.Find( x => x.userName == text);
        Managers.Game.AddHostage(findUser);
    }
}