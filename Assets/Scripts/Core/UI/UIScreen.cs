using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIScreen : UIWindow
{
    // Gauge를 통해서 자동으로 Screen을 다음으로 넘기는지 여부
    // True -> 자동으로 넘김
    // False -> 자동으로 넘기지 않음
    [field: SerializeField]
    public bool useAutoNextPage { get; private set; }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        return true;
    }
}
