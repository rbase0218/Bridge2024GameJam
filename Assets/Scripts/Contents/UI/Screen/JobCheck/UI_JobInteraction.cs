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

    private enum JobInfo 
    {
        NameTagContainer
    }

    private enum Card
    {
        CardContainer
    }

    private enum Select
    {
        SelectContainer
    }
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;
        
        BindObject(typeof(Boards));
        Bind<UINameTagContainer>(typeof(JobInfo));
        Bind<UICardContainer>(typeof(Card));
        Bind<UISelectContainer>(typeof(Select));

        return true;
    }
    
    protected override bool EnterWindow()
    {
        return true;
    }
}
