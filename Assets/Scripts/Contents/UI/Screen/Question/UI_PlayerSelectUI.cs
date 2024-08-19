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
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        Bind<UIPlayerSelector>(typeof(PlayerSelector));
        Get<UIPlayerSelector>((int)PlayerSelector.SelectContainer).Binding();
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var selectContainer = Get<UIPlayerSelector>((int)PlayerSelector.SelectContainer);
        
        //selectContainer.ShowButton(Managers.Game._userList.Select((x) => x.userName).ToArray());
        selectContainer.onClickSubmitButton.AddListener(OnClickSubmitButton);
        
        return true;
    }

    private void OnClickSubmitButton(Button button)
    {
        // 질문 전달자 확인.
        // 다음 Scene으로 이동한다.
        OnNextScreen<UI_TextConfirm01>();
    }
}
