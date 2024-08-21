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
        Debug.Log("이번 차례는 " +Managers.Game.currentUser.userName);
        
        // 자기 자신을 제외한 리스트를 가져온다.
        selectContainer.ShowButton(Managers.Game.GetAnswerUserList().Select(user => user.userName).ToArray());

        GetText((int)Texts.Text).text = Managers.Game.currentUser.userName;
        selectContainer.onClickSubmitButton.AddListener(OnClickSubmitButton);
        
        return true;
    }

    private void OnClickSubmitButton(string text)
    {
        // 질문 전달자 확인.
        Managers.Game.selectUserName = text;
        // 다음 Scene으로 이동한다.
        OnNextScreen<UI_TextConfirm01>();
    }
}
