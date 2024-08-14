using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIScreen : UIWindow
{
    // Gauge를 통해서 자동으로 Screen을 다음으로 넘기는지 여부
    // True -> 자동으로 넘김
    // False -> 자동으로 넘기지 않음
    [field: SerializeField]
    public bool UseAutoNextScreen { get; private set; } = true;
    
    protected UI_Gauge _gauge;

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        _gauge = GameObject.Find("Gauge").GetComponent<UI_Gauge>();
        return true;
    }
    
    public void SetAutoNextPage(bool isAuto) => UseAutoNextScreen = isAuto;
    
    public void BindNextScreen<T>() where T : UIScreen
    {
        _gauge.onEndGauge.RemoveAllListeners();
        
        _gauge.onEndGauge.AddListener(() =>
        {
            Managers.UI.CloseWindow();
            Managers.UI.ShowWindow<T>();
        });
        
        _gauge.Play();
    }

    public void OnNextScreen<T>() where T : UIScreen
    {
        _gauge.onEndGauge.RemoveAllListeners();
        
        Managers.UI.CloseWindow();
        Managers.UI.ShowWindow<T>();
    }
}
