using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame8 : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(AnimDelay), 1.5f);
    }

    private void OnDisable()
    {
        
    }

    private void AnimDelay()
    {
        RoundManager.instance.OpenFrameThis(9);
    }
}
