using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSelectUIV : UIScreen
{
    private bool isSelect;
    private List<UserInfo> copyUserList = new List<UserInfo>();
    private enum Objects
    {
        Board_A,
        Board_B
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
        Bind<UIPlayerSelector>(typeof(PlayerSelector));
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA).Binding();
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerB).Binding();
        
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA).onClickSubmitButton.AddListener(OnClickSubmitButtonA);
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerB).onClickSubmitButton.AddListener(OnClickSubmitButtonB);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        if(UseAutoNextScreen)
            BindNextScreen<UI_ClockSwitcherV>();
        
        isSelect = false;
        var currentUser = Managers.Game.currentUser;
        if (currentUser.jobType == EJobType.Assassin)
        {
            copyUserList = new List<UserInfo>(Managers.Game._userList);
            copyUserList.RemoveAll(x => Managers.Game._hostageList.Contains(x) || x.isDie);
            
            GetObject((int)Objects.Board_B).SetActive(true);
            GetObject((int)Objects.Board_A).SetActive(false);

            var selectorB = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerB);
            selectorB.ShowButton(copyUserList.Select(x => x.userName).ToArray());
            
            _gauge.onEndGauge.AddListener(() =>
            {
                if (isSelect)
                    return;
             
                RandomSubmit();
            });
            
            GetText((int)Texts.FrontText).SetText("당신은");
            GetText((int)Texts.Text).SetText("암살자");
            GetText((int)Texts.BackText).SetText("입니다.");
        }
        else
        {
            var userList = Managers.Game._userList.FindAll((x) => x.userName != Managers.Game.currentUser.userName);
            var aliveUserArray = userList.ToList().FindAll(x => x.isDie == false).Select(x => x.userName).ToArray();
            
            GetObject((int)Objects.Board_B).SetActive(false);
            GetObject((int)Objects.Board_A).SetActive(true);
            
            var selectorA = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA);
            selectorA.ShowButton(aliveUserArray);
            
            GetText((int)Texts.FrontText).SetText("이번 투표 순서는");
            GetText((int)Texts.Text).SetText(currentUser.userName);
            GetText((int)Texts.BackText).SetText("입니다.");
        }
        
        return true;
    }

    // 광대 및 귀빈
    private void OnClickSubmitButtonA(string text)
    {
        if (text == null)
            return;
        
        // 해당 유저에 대한 정보를 찾는다.
        var findUser = Managers.Game._userList.Find( x => x.userName == text);
        Managers.Game.AddVoteUser(findUser);
        
        OnNextScreen<UI_ClockSwitcherV>();
    }

    // 암살자
    private void OnClickSubmitButtonB(string text)
    {
        if (text == null)
            return;
        
        var findUser = Managers.Game._userList.Find( x => x.userName == text);
        Managers.Game.AddHostage(findUser);
        OnNextScreen<UI_ClockSwitcherV>();
        isSelect = true;
    }
    
    private void RandomSubmit()
    {
        var random = Random.Range(0, copyUserList.Count);
        var selectUser = copyUserList[random];
        //Debug.Log("랜덤 선택 : " + selectUser.userName);
        Managers.Game.AddHostage(selectUser);
        isSelect = true;
    }
}