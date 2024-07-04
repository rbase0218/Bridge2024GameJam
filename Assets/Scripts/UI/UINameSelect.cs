using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINameSelect : UIWindow
{
    private enum Buttons
    {
        EntryButton
    }

    private enum RectTransforms
    {
        Content
    }
    
    [Space(10), SerializeField]
    private GameObject rootContent;

    [SerializeField]
    private GameObject childObj;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        Bind<RectTransform>(typeof(RectTransforms));
        
        GetButton((int)Buttons.EntryButton).onClick.AddListener(OnClickEntryButton);

        return true;
    }

    protected override void Setting()
    {
        Get<RectTransform>((int)RectTransforms.Content).anchoredPosition = Vector2.zero;
    }

    public void ShowChildren(int count)
    {
        if (count < 0)
            return;

        for (int i = 0; i < rootContent.transform.childCount; ++i)
        {
            if(i < count)
                rootContent.transform.GetChild(i).gameObject.SetActive(true);
            else
                rootContent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnClickEntryButton()
    {
        Debug.Log("Click - Entry Button");
        Managers.UI.CloseWindow();
    }
}
