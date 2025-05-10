using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSelectUIV : UIScreen
{
    private bool isSelect;
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
        
        var currentUser = Managers.Game.GetCurrentPlayer();
        
        if (currentUser.jobType == EJobType.Assassin)
        {
            var notHostagePlayers = Managers.Game.GetAllPlayers().FindAll( x => x.isHostage != true).Select(x => x.userName).ToArray();
            
            GetObject((int)Objects.Board_B).SetActive(true);
            GetObject((int)Objects.Board_A).SetActive(false);
        
            var selectorB = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerB);
            selectorB.ShowButton(notHostagePlayers);
            
            _gauge.onEndGauge.AddListener(() =>
            {
                if (isSelect)
                    return;
             
                RandomSubmit(notHostagePlayers);
            });
            
            GetText((int)Texts.FrontText).SetText("당신은");
            GetText((int)Texts.Text).SetText("암살자");
            GetText((int)Texts.BackText).SetText("입니다.");
        }
        else
        {
            // 남아 있는 플레이어 중, 살아 있는 플레이어 목록
            var alivePlayers = Managers.Game.GetAllPlayers().FindAll( x => !x.isDie).Select(x => x.userName).ToArray();
            
            GetObject((int)Objects.Board_B).SetActive(false);
            GetObject((int)Objects.Board_A).SetActive(true);
            
            var selectorA = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA);
            selectorA.ShowButton(alivePlayers);
            
            GetText((int)Texts.FrontText).SetText("이번 투표 순서는");
            GetText((int)Texts.Text).SetText(currentUser.userName);
            GetText((int)Texts.BackText).SetText("입니다.");
        }
        
        return true;
    }

    // 광대 및 귀빈
    private void OnClickSubmitButtonA(string text)
    {
        Managers.Sound.PlaySFX("Click");

        if (text == null)
            return;
        
        // 해당 유저에 대한 정보를 찾는다.
        var findUser = Managers.Game.FindPlayer(text);
        // Managers.Game.AddVoteUser(findUser);
        
        OnNextScreen<UI_ClockSwitcherV>();
    }

    // 암살자
    private void OnClickSubmitButtonB(string text)
    {
        Managers.Sound.PlaySFX("Click");

        if (text == null)
            return;

        var findUser = Managers.Game.FindPlayer(text);
        
        Managers.Game.AddHostage(findUser);
        
        OnNextScreen<UI_ClockSwitcherV>();
        isSelect = true;
    }
    
    private void RandomSubmit(params string[] playerNames)
    {
        var random = Random.Range(0, playerNames.Length);
        var selectUser = playerNames[random];
        
        Debug.Log("랜덤 선택 : " + selectUser);
        Managers.Game.AddHostage(selectUser);
        isSelect = true;
    }
}