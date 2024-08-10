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
        return true;
    }

    public void SetInfo(UI_Gauge gauge)
    {
        gauge.onEndGauge.AddListener(() =>
        {
            OnNextPage(gauge);
        });
    }

    private void OnNextPage(UI_Gauge gauge)
    {
        var sequence = Managers.UI.GetWindow<UI_Sequence01>();
        // 1) Data Protocol을 정립한다
        // 2) Game 데이터를 직접적으로 전달해주는 방식을 사용한다.
        sequence.SetInfo(null);
    }
}
