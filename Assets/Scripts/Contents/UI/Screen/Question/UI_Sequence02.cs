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
        Get<UIPersonViewer>((int)PersonViewer.PersonViewer).SetFrame(
            new FrameData("Title1", "Name1", 1),
            new FrameData("Title2", "Name2", 0)
        );
        
        if(UseAutoNextScreen)
            BindNextScreen<UI_QuestionInput>();
        
        return true;
    }
}
