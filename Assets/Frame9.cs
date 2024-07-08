using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame9 : MonoBehaviour
{
    private Button button;
    private bool isClicked;
    
    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        if (!isClicked)
        {
            RoundManager.instance.uiWordCheck.OpenCard();
            isClicked = true;
        }
        else
        {
            RoundManager.instance.OpenWordCheck();
        }
    }
}
