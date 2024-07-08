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
        if (isClicked)
        {
            isClicked = false;
            RoundManager.instance.uiThis._frame9Card.Reset();
            RoundManager.instance.OpenWordCheck();
        }
        else
        {
            isClicked = true;
            RoundManager.instance.uiThis.OpenFrame9();
        }
    }
}
