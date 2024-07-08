using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame20 : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(AnimDelay), 3f);
    }

    private void OnDisable()
    {
        
    }

    private void AnimDelay()
    {
        RoundManager.instance.PresentationRound();
    }
}
