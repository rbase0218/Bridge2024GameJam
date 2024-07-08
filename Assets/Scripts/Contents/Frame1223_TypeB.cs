using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Frame1223_TypeB : MonoBehaviour
{
    private Button button;
    private bool isClicked;
    private int curIndex;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
    }
    
    public void OnClickSelectButton(int num)
    {
        curIndex = num;
    }

    public void OnClickSendButton()
    {
        RoundManager.instance.SetCurHostage(curIndex);
        RoundManager.instance.NextWordCheckUser();
    }
}
