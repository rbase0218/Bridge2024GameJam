using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSelectUI : UIScreen
{
    private enum Texts
    {
        NameText
    }
    
    private enum PlayerSelector
    {
        SelectContainer
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        //BindText(typeof(Texts));

        Bind<UIPlayerSelector>(typeof(PlayerSelector));
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainer).Binding();
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var selectContainer = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainer);
        //GetText((int)Texts.NameText).text = Managers.Game.currentUser.userName;
        Debug.Log("이번 차례는 " +Managers.Game.currentUser.userName);
        
        selectContainer.ShowButton(Managers.Game.GetAnswerUserList().Select(user => user.userName).ToArray());
        selectContainer.onClickSubmitButton.AddListener(OnClickSubmitButton);
        
        return true;
    }

    private void OnClickSubmitButton(Button button)
    {
        // 질문 전달자 확인.
        // 다음 Scene으로 이동한다.
        Managers.Game.SetAnswerUser(button.GetComponentInChildren<Text>().text);
        OnNextScreen<UI_TextConfirm01>();
    }
}
