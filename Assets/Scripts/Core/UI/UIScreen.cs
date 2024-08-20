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

    protected UnityEvent onSceneChanged;

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
        _gauge.onEndGauge.AddListener(OnSceneChanged<T>);
        
        _gauge.Play();
    }

    public T OnNextScreen<T>() where T : UIScreen
    {
        _gauge.Stop();
        
        OnSceneChanged<T>();
        return Managers.UI.GetWindow<T>();
    }

    private void OnSceneChanged<T>() where T: UIScreen
    {
        onSceneChanged?.Invoke();
        onSceneChanged?.RemoveAllListeners();
        
        Managers.UI.CloseWindow();
        Managers.UI.ShowWindow<T>();
    }
}
