using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame23 : MonoBehaviour
{
    private Button button;
    private bool isClicked;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        if (RoundManager.instance.curPageType == EPageType.Question)
        {
            GetComponent<Frame12_TypeA>().enabled = false;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
            UIGauge.instance.onEndGauge.RemoveAllListeners();
            RoundManager.instance.uiWordCheck.RemoveGauge();
        }
    }

    private void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            RoundManager.instance.uiWordCheck.OpenCard();
            RoundManager.instance.GoNextAnswerPage();
            UIGauge.instance.onEndGauge.AddListener(RoundManager.instance.GoAnswerSelectedPage);
            RoundManager.instance.uiWordCheck.StartGauge();
        }
    }
}