using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

public class UI_Introduce_Target : UIScreen
{
    private enum Boards
    {
        Board_A
    }

    private enum Texts
    {
        JobNameText,
        Text
    }

    private enum Buttons
    {
        CardAnim
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindObject(typeof(Boards));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        return true;
    }

    protected override bool EnterWindow()
    {
        var cardAnim = GetButton((int)Buttons.CardAnim);
        cardAnim.interactable = true;
        cardAnim.GetComponent<MMTwoSidedUI>().Front.SetActive(false);
        cardAnim.GetComponent<MMTwoSidedUI>().Back.SetActive(true);

        if (UseAutoNextScreen)
            BindNextScreen<UI_Switcher01_Last2>();
        var playerName = Managers.Game.GetCurrentPlayer().userName;
        
        GetText((int)Texts.Text).SetText(playerName);
        GetText((int)Texts.JobNameText).SetText(Managers.Game.GetFinalVoteTarget().userName);

        var cardButton = GetButton((int)Buttons.CardAnim);
        if (cardButton != null && !Utils.HasListener(cardButton.onClick, OnClickOpenCardButton))
        {
            cardButton.onClick.AddListener(OnClickOpenCardButton);
        }

        return true;
    }

    private void OnClickOpenCardButton()
    {
        Managers.Sound.PlaySFX("Card");
        BindNextScreen<UI_Switcher01_Last2>();
        GetButton((int)Buttons.CardAnim).interactable = false;
    }
}