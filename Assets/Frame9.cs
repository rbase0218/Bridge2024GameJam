using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame9 : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        if (RoundManager.instance.uiWordCheck.IsCardActive())
        {
            RoundManager.instance.uiWordCheck.OpenCard();
        }
        else
        {
            RoundManager.instance.OpenWordCheck();
        }
    }
}
