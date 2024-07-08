using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frame20 : MonoBehaviour
{
    private bool onceTime;

    private void OnEnable()
    {
        if (!onceTime)
        {
            onceTime = true;
            Invoke(nameof(AnimDelay), 3f);
        }
    }

    private void OnDisable()
    {
    }

    private void AnimDelay()
    {
        RoundManager.instance.PresentationRound();
    }
}