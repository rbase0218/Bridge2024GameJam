using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Sequence02 : UIScreen
{
    private enum PersonViewer
    {
        PersonViewer
    }
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        Bind<UIPersonViewer>(typeof(PersonViewer));
        Get<UIPersonViewer>((int)PersonViewer.PersonViewer).BindInstance();
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var hostageUser = Managers.Game.hostageUser;
        
        Managers.Game.GetQuestionUser();
        // Get<UIPersonViewer>((int)PersonViewer.PersonViewer).SetFrame(
        //     new FrameData("인질", hostageUser.userName, 1),
        //     new FrameData("질문", questionUser.userName, 0)
        // );
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_QuestionInput>();
        
        return true;
    }
}
