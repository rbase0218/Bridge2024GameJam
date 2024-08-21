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
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        Managers.Game.GetQuestionUser();
        var hostageName = Managers.Game.GetCurrentHostage().userName;
        var currUserName = Managers.Game.currentUser.userName;
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
        OnNextScreen<UI_QuestionInput>();
    }
}
