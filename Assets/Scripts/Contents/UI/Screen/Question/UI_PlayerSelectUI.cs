using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSelectUI : UIScreen
{
    private enum PlayerSelector
    {
        SelectContainer
    }

    private enum Texts
    {
        Text
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        //BindText(typeof(Texts));

        Bind<UIPlayerSelector>(typeof(PlayerSelector));
        BindText(typeof(Texts));
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainer).Binding();
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var selectContainer = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainer);
        //GetText((int)Texts.NameText).text = Managers.Game.currentUser.userName;
        //Debug.Log("이번 차례는 " +Managers.Game.currentUser.userName);
        
        // 질문자 목록을 만들어야 한다.
        var currUserName = Managers.Game.GetCurrentPlayer().userName;
        var answerUserList = Managers.Game.GetAllPlayers(currUserName);
        selectContainer.ShowButton(answerUserList.Select(user => user.userName).ToArray());
        
        // 현재 유저의 이름을 표출한다.
        GetText((int)Texts.Text).text = currUserName;
        
        selectContainer.onClickSubmitButton.AddListener(OnClickSubmitButton);
        return true;
    }

    private void OnClickSubmitButton(string text)
    {
        Managers.Sound.PlaySFX("Click");
        if (text == null)
            return;
        
        // // 질문 전달자 확인.
        // Managers.Game.selectUserName = text;
        // // 다음 Scene으로 이동한다.
        // Managers.Game.SetAnswerUser(text);
        // OnNextScreen<UI_AnswerPerson>();
    }
}
