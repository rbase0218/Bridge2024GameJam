using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelectButtons : UIBase
{
    public enum Buttons
    {
        Button1,
        Button2,
        Button3,
        Button4,
        Button5,
        Button6
    }
    
    private int clickButtonIndex = -1;

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));

        return true;
    }
    
    public void SetInfo(params string[] buttonTexts)
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            GetButton(i).GetComponentInChildren<TMP_Text>().text = buttonTexts[i];
            GetButton(i).gameObject.SetActive(true);
            GetButton(i).onClick.AddListener(() =>
            {
                OnClickButton(i);
            });
        }
    }

    public void OnClickButton(int i)
    {
        clickButtonIndex = i;
    }

    public Button GetClickButton()
    {
        if(clickButtonIndex == -1)
            return null;
        return GetButton(clickButtonIndex);
    }
}
