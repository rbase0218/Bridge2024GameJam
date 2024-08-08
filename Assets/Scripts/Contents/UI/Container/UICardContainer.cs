using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardContainer : UIBase
{
    public enum Objects
    {
        OpenCard,
        CloseCard
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindObject(typeof(Objects));
        return true;
    }

    public void Open()
    {
        GetObject((int)Objects.OpenCard).SetActive(false);
    }
}
