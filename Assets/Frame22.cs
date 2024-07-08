using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame22 : MonoBehaviour
{
    private Button button;
    private bool isClicked;
    private int curIndex;

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
        RoundManager.instance.SetAnswerTarget(curIndex);
        
        if(UIGauge.instance.isPlaying)
            RoundManager.instance.GoTimeWaitFrame();
    }
}