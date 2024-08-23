using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_JobInteraction : UIScreen
{
    private bool isSelect;
    private enum Boards
    {
        Board_A,
        Board_B
    }
    
    private enum Texts
    {
        WordText,
        Text
    }

    private enum Buttons
    {
        CloseCard
    }
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
        _gauge.SetGauge(10f);
        isSelect = false;
        GetButton((int)Buttons.CloseCard).gameObject.SetActive(true);
        
        if (UseAutoNextScreen)  
            BindNextScreen<UI_ClockSwitcher>();
        
        
         //1. 현재 진행중인 유저의 정보를 가져온다.
        var user = Managers.Game.currentUser;

        GetText((int)Texts.Text).text = Managers.Data.GetJobText(user.jobType);
        
         //2. 유저의 정보에 따라서 Board A와 Board B를 활성화한다.
         if (user.jobType == EJobType.Actor || user.jobType == EJobType.VIP)
         {
             GetObject((int)Boards.Board_A).SetActive(true);
             GetObject((int)Boards.Board_B).SetActive(false);
        
             // 주제를 이 곳에서 전달한다.
             GetText((int)Texts.WordText).text = Managers.Game.gameTopic;
             
             GetButton((int)Buttons.CloseCard).onClick.AddListener(OnClickOpenCardButton);
         }
         else
         {
             var userList = Managers.Game._userList.FindAll((x) => x.userName != Managers.Game.currentUser.userName).Select( (x) => x.userName).ToArray();
             _playerSelector.ShowButton(userList);
             _playerSelector.onClickSubmitButton.AddListener(OnClickSubmitButton);
             
             GetObject((int)Boards.Board_B).SetActive(true);
             GetObject((int)Boards.Board_A).SetActive(false);
             
             _gauge.onEndGauge.AddListener(() =>
             {
                 if (isSelect)
                     return;
             
                 RandomSubmit();
             });
         }
        
        return true;
    }

    private void OnClickSubmitButton(string text)
    {
        if (text == null)
            return;
        
        _playerSelector.onClickSubmitButton.RemoveAllListeners();
        
        // Hostage를 해당 기능을 통해서 선택한다.
        var selectUserName = text;
        var selectUser = Managers.Game._userList.Find((x) => x.userName == selectUserName);
        
        //Debug.Log(selectUserName + " : " + selectUser.userName);
        
        Managers.Game.AddHostage(selectUser);

        OnNextScreen<UI_ClockSwitcher>();
        isSelect = true;
    }
    
    private void RandomSubmit()
    {
        var random = Random.Range(0, Managers.Game._userList.Count);
        var selectUser = Managers.Game._userList[random];
        //Debug.Log("랜덤 선택 : " + selectUser.userName);
        _playerSelector.onClickSubmitButton.RemoveAllListeners();
        Managers.Game.AddHostage(selectUser);
        isSelect = true;
    }

    private void OnClickOpenCardButton()
    {
        GetButton((int)Buttons.CloseCard).gameObject.SetActive(false);
        GetButton((int)Buttons.CloseCard).onClick.RemoveAllListeners();
        
        BindNextScreen<UI_ClockSwitcher>();
    }
}
