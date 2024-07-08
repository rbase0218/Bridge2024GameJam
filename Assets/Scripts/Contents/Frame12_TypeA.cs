using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame12_TypeA : MonoBehaviour
{
    private Button button;
    private bool isClicked;
    
    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnClick);
        UIGauge.instance.onEndGauge.RemoveAllListeners();
        UIGauge.instance.onEndGauge.AddListener(RoundManager.instance.NextWordCheckUser);
    }
    
    private void OnClick()
    {
        if (isClicked)
        {
            isClicked = false;
            RoundManager.instance.uiWordCheck.ResetCard();
            
            if (UIGauge.instance.isPlaying)
            {
                RoundManager.instance.GoTimeWaitFrame();
            }
        }
        else
        {
            isClicked = true;
            RoundManager.instance.uiWordCheck.OpenCard();
        }
    }
    
    
}
