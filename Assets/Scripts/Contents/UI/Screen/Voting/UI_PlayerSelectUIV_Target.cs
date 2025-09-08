using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSelectUIV_Target : UIScreen
{
    private bool isSelect;
    private enum Objects
    {
        Board_A,
    }

    private enum Texts
    {
        FrontText,
        NameText,
        BackText
    }

    private enum PlayerSelector
    {
        SelectContainerA
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        Bind<UIPlayerSelector>(typeof(PlayerSelector));
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA).Binding();

        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA).onClickSubmitButton.AddListener(OnClickSubmitButtonA);

        return true;
    }

    protected override bool EnterWindow()
    {
        if (UseAutoNextScreen)
            BindNextScreen<UI_Switcher01_Last>();

        var inGameSetting = Managers.UI.GetWindow<UI_InGameSetting>();
        if (inGameSetting != null)
        {
            inGameSetting.HideEmergencyButton();
        }

        isSelect = false;

        var currentUser = Managers.Game.GetCurrentPlayer();

        // 남아 있는 플레이어 중, 살아 있는 플레이어 목록 (발의자 제외)
        var proposer = Managers.Game.GetFinalVoteProposer();
        var alivePlayers = Managers.Game.GetAllPlayers()
            .FindAll(x => !x.isDie && x != proposer)
            .Select(x => x.userName).ToArray();

        GetObject((int)Objects.Board_A).SetActive(true);

        var selectorA = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainerA);
        selectorA.ShowButton(alivePlayers);

        GetText((int)Texts.FrontText).SetText("이번 투표 순서는");
        GetText((int)Texts.NameText).SetText(proposer.userName);
        GetText((int)Texts.BackText).SetText("입니다.");

        return true;
    }

    // 광대 및 귀빈
    private void OnClickSubmitButtonA(string text)
    {
        Managers.Sound.PlaySFX("Click");
        
        if (text == null)
            return;

        // 최후 투표 지목 대상 설정
        var findUser = Managers.Game.FindPlayer(text);
        Managers.Game.SetFinalVoteTarget(findUser);

        OnNextScreen<UI_Switcher01_Last>();
    }
}