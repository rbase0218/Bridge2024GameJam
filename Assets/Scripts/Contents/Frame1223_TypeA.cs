using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame1223_TypeA : MonoBehaviour
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
            RoundManager.instance.NextWordCheckUser();
        }
        else
        {
            isClicked = true;
            RoundManager.instance.uiWordCheck.OpenCard();
        }
    }
}
