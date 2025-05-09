using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Sequence02 : UIScreen
{
    private enum PersonViewer
    {
        PersonViewer
    }
    
    private enum Buttons
    {
        NextButton
    }
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        Bind<UIPersonViewer>(typeof(PersonViewer));
        Get<UIPersonViewer>((int)PersonViewer.PersonViewer).BindInstance();
        BindButton(typeof(Buttons));
        var nextButton = GetButton((int)Buttons.NextButton);
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(OnClickNextButton);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        // 여기서 오류 생김. 질문 할 얘가 없어서 null 반환 -> _userList[0]을 줌.
        string hostageName = Managers.Game.GetCurrentHostageName().userName;
        string currUserName = Managers.Game.GetCurrentPlayer().userName;
        
        Get<UIPersonViewer>((int)PersonViewer.PersonViewer).SetFrame(
            new FrameData("인질", hostageName, 1),
            new FrameData("첫 순서", currUserName, 0)
        );
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_QuestionInput>();
        
        return true;
    }
    
    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");
        OnNextScreen<UI_QuestionInput>();
    }
}
