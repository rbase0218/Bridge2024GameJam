using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardInfoContainer : UIBase
{
    public enum Card
    {
        CardContainer
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<UICardContainer>(typeof(Card));

        return true;
    }

    public void Open()
    {
        Get<UICardContainer>((int)Card.CardContainer).Open();
    }
}
