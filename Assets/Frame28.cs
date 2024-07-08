using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Frame28 : MonoBehaviour
{
    private Button button;
    private bool isClicked;
    private int curIndex;
    private string name;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        UIGauge.instance.onEndGauge.RemoveAllListeners();
        UIGauge.instance.onEndGauge.AddListener(RoundManager.instance.QuestionSelectPage);
    }
    
    public void OnClickSelectButton(int num)
    {
        curIndex = num;
    }

    public void OnClickSendButton()
    {
        RoundManager.instance.SetAnswerTarget(name);
        
        if(UIGauge.instance.isPlaying)
            RoundManager.instance.GoTimeWaitFrame();
    }
}
