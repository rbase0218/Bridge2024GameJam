using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JobInteraction : UIScreen
{
    private enum Boards
    {
        Board_A,
        Board_B
    }

    #region # [ Board B Component ] #
    
    private UIPlayerSelector _playerSelector;
    
    #endregion
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;
        
        BindObject(typeof(Boards));
        _playerSelector = Utils.FindChild<UIPlayerSelector>(gameObject, "PlayerSelect", true);
        _playerSelector.Binding();

        return true;
    }
    
    protected override bool EnterWindow()
    {
        // 1. 현재 진행중인 유저의 정보를 가져온다.
        var user = Managers.Game._currentUser;
        
        // 2. 유저의 정보에 따라서 Board A와 Board B를 활성화한다.
        // if (user.jobType == EJobType.Actor || user.jobType == EJobType.VIP)
        // {
        //     GetObject((int)Boards.Board_A).SetActive(true);
        //     GetObject((int)Boards.Board_B).SetActive(false);
        // }
        // else
        {
            GetObject((int)Boards.Board_B).SetActive(true);
            GetObject((int)Boards.Board_A).SetActive(false);
            
        }

        return true;
    }
}