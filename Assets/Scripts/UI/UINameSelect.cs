using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINameSelect : UIWindow
{
    private enum Buttons
    {
        EntryButton
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
        GetButton((int)Buttons.EntryButton).onClick.AddListener(OnClickEntryButton);

        return true;
    }

    public void MakeChildren(int count)
    {
        RemoveChildren();
        Utils.MakeChildren<TMP_Text>(rootContent.transform, childObj, count);
    }

    private void RemoveChildren()
    {
        Debug.Log(rootContent.transform.childCount);
    }

    private void OnClickEntryButton()
    {
        Debug.Log("Click - Entry Button");
        Managers.UI.CloseWindow();
    }
}
