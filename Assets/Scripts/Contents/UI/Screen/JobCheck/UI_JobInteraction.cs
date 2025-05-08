using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        // var user = Managers.Game.currentUser;

        // GetText((int)Texts.Text).text = Managers.Data.GetJobText(user.jobType);
        //
        //  //2. 유저의 정보에 따라서 Board A와 Board B를 활성화한다.
        //  if (user.jobType == EJobType.Actor || user.jobType == EJobType.VIP)
        //  {
        //      GetObject((int)Boards.Board_A).SetActive(true);
        //      GetObject((int)Boards.Board_B).SetActive(false);
        //
        //      // 주제를 이 곳에서 전달한다.
        //      string originalText = Managers.Game.gameTopic;
        //      string wrappedText = string.Join("\n", 
        //          Enumerable.Range(0, (originalText.Length + 6) / 7)
        //              .Select(i => originalText.Substring(i * 7, 
        //                  Math.Min(6, originalText.Length - i * 7))));
        //      GetText((int)Texts.WordText).text = wrappedText;
        //      GetButton((int)Buttons.CloseCard).onClick.AddListener(OnClickOpenCardButton);
        //  }
        //  else
        //  {
        //      //BindNextScreen<UI_ClockSwitcher>();
        //      var userList = Managers.Game._userList.Select( (x) => x.userName).ToArray();
        //      _playerSelector.ShowButton(userList);
        //      _playerSelector.onClickSubmitButton.AddListener(OnClickSubmitButton);
        //      
        //      GetObject((int)Boards.Board_B).SetActive(true);
        //      GetObject((int)Boards.Board_A).SetActive(false);
        //      
        //      _gauge.onEndGauge.AddListener(() =>
        //      {
        //          if (isSelect)
        //              return;
        //      
        //          RandomSubmit();
        //      });
        //  }
        
        return true;
    }

    private void OnClickSubmitButton(string text)
    {
        Managers.Sound.PlaySFX("Click");
        if (text == null)
            return;
        
        _playerSelector.onClickSubmitButton.RemoveAllListeners();
        
        // Hostage를 해당 기능을 통해서 선택한다.
        var selectUserName = text;
        // var selectUser = Managers.Game._userList.Find((x) => x.userName == selectUserName);
        //
        // //Debug.Log(selectUserName + " : " + selectUser.userName);
        //
        // Managers.Game.AddHostage(selectUser);

        OnNextScreen<UI_ClockSwitcher>();
        isSelect = true;
    }
    
    private void RandomSubmit()
    {
        // var random = Random.Range(0, Managers.Game._userList.Count);
        // var selectUser = Managers.Game._userList[random];
        // //Debug.Log("랜덤 선택 : " + selectUser.userName);
        // _playerSelector.onClickSubmitButton.RemoveAllListeners();
        // Managers.Game.AddHostage(selectUser);
        isSelect = true;
    }

    private void OnClickOpenCardButton()
    {
        Managers.Sound.PlaySFX("Card"); 
        GetButton((int)Buttons.CloseCard).gameObject.SetActive(false);
        GetButton((int)Buttons.CloseCard).onClick.RemoveAllListeners();
        
        BindNextScreen<UI_ClockSwitcher>();
    }
}
