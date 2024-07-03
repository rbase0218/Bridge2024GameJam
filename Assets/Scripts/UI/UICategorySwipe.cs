using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICategorySwipe : UISwipe
{
    private string[] data;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        data = Managers.Data.categoryArray;
        count = 0;
        GetText((int)Texts.SwipeValue).text = data[count];
        
        return true;
    }

    protected override void OnClickAfterButton()
    {
        Debug.Log("Click After");
        if (count + 1 > data.Length)
            return;

        count += 1;
        RefreshUI();
    }

    protected override void OnClickBeforeButton()
    {
        Debug.Log("Click Before");
        
        if (count - 1 < 0)
            return;

        count -= 1;
        RefreshUI();
    }

    protected override void RefreshUI()
    {
        GetText((int)Texts.SwipeValue).text = data[count];
    }
}