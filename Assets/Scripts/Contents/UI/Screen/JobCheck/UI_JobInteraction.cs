using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_JobInteraction : UIScreen
{
    private enum Boards
    {
        Board_A,
        Board_B
    }
    
    #region # [ Board A Component ] #
    
    private enum Texts
    {
        WordText
    }

    private enum Buttons
    {
        CloseCard
    }
    
    #endregion
    #region # [ Board B Component ] #
    
    private UIPlayerSelector _playerSelector;
    
    #endregion
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;
        
        BindObject(typeof(Boards));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        _playerSelector = Utils.FindChild<UIPlayerSelector>(gameObject, "PlayerSelect", true);
        _playerSelector.Binding();

        return true;
    }
    
    protected override bool EnterWindow()
    {
        if (UseAutoNextScreen)
            BindNextScreen<UI_ClockSwitcher>();
        
        // 1. 현재 진행중인 유저의 정보를 가져온다.
        //var user = Managers.Game._currentUser;
        
        // 2. 유저의 정보에 따라서 Board A와 Board B를 활성화한다.
        // if (user.jobType == EJobType.Actor || user.jobType == EJobType.VIP)
        // {
        //     GetObject((int)Boards.Board_A).SetActive(true);
        //     GetObject((int)Boards.Board_B).SetActive(false);
        //
        //     GetText((int)Texts.WordText).text = "당신은 VIP입니다.";
        //     GetButton((int)Buttons.CloseCard).onClick.AddListener(OnClickOpenCardButton);
        // }
        // else
        // {
        //     _playerSelector.ShowButton(Managers.Game._userList.Select((x) => x.userName).ToArray());
        //     _playerSelector.onClickSubmitButton.AddListener(OnClickSubmitButton);
        //     
        //     GetObject((int)Boards.Board_B).SetActive(true);
        //     GetObject((int)Boards.Board_A).SetActive(false);
        // }
        
        return true;
    }

    private void OnClickSubmitButton(Button button)
    {
        // Hostage를 해당 기능을 통해서 선택한다.
        var selectUserName = button.GetComponentInChildren<TMP_Text>().GetParsedText();
        var selectUser = Managers.Game._userList.Find((x) => x.userName == selectUserName);
        Managers.Game.AddHostage(selectUser);
        Debug.Log(selectUserName + " : " + selectUser.userName);
        _playerSelector.onClickSubmitButton.RemoveAllListeners();
    }

    private void OnClickOpenCardButton()
    {
        GetButton((int)Buttons.CloseCard).gameObject.SetActive(false);
        GetButton((int)Buttons.CloseCard).onClick.RemoveAllListeners();
    }
}
