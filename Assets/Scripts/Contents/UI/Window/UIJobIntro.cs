using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJobIntro : UIWindow
{
    public enum NextOrder
    {
        NextOrderContainer
    }

    public enum CardInfo
    {
        CardInfoContainer
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<UINextOrderContainer>(typeof(NextOrder));
        Bind<UICardInfoContainer>(typeof(CardInfo));

        return true;
    }
    
    protected override bool EnterWindow()
    {
        throw new System.NotImplementedException();
    }

    public void SetInfo(string name, string frontText = "", string backText = "")
    {
        Get<UINextOrderContainer>((int)NextOrder.NextOrderContainer).SetInfo(name, frontText, backText);
    }

    public void Open()
    {
        Get<UICardInfoContainer>((int)CardInfo.CardInfoContainer).Open();
    }
}
