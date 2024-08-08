using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISubmitButton : UIBase
{
    private Button submitButton;
    public UnityEvent onClickSubmit;
    
    public enum Texts
    {
        Text
    }
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        BindText(typeof(Texts));
        submitButton = Utils.TryOrAddComponent<Button>(gameObject);
        
        submitButton.onClick.AddListener(() =>
        {
            onClickSubmit?.Invoke();
        });
        
        return true;
    }
}
