using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameScene : Framework
{
    // 해당 메서드는 Awake에서 실행된다.
    // Data를 SetUp 할 때 필요한 메서드.
    protected override void SetUp()
    {
        
    }

    // 해당 메서드는 Start에서 로직을 관리할 때 사용된다.
    protected override void Start()
    {
        Debug.Log("Call");
    }
}
