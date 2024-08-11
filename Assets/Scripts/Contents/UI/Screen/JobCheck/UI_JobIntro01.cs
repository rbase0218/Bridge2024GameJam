using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JobIntro01 : UIScreen
{
    private enum NextOrder
    {
        NextOrderContainer
    }

    private enum CardInfo
    {
        CardInfoContainer
    }
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;
        
        Bind<UINameTagContainer>(typeof(NextOrder));
        Bind<UICardInfoContainer>(typeof(CardInfo));
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        return true;
    }
}
