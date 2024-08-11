using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Intro01 : UIScreen
{
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        // Auto Page 활성화
        SetAutoNextPage(true);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        if (UseAutoNextScreen)
        {
            _gauge.onEndGauge.AddListener(() =>
            {
                Managers.UI.ShowWindow<UI_Sequence01>();
            });
            
            _gauge.Play();
        }
        
        return true;
    }
}
