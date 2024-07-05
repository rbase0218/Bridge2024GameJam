using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private TMP_Text textTMP;
    private Image image;
    private Color defaultColor;
    private bool isSelected;
    private bool isLocked;
    public bool IsSelected => isSelected;

    private void OnValidate()
    {
        textTMP = transform.GetChild(0).GetComponent<TMP_Text>();
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    private void Start()
    {
        image.color = Color.blue;
    }

    public void SetText(string text)
    {
        textTMP.text = text;
    }
    
    public void Select()
    {
        image.color = Color.blue;
        isSelected = true;
    }
    
    public void Reset()
    {
        if(isLocked)
            return;
        
        image.color = Color.blue;;
        isSelected = false;
    }
    
    public void UnSelect()
    {
        if (isLocked)
            return;
        
        image.color = Color.gray;
        isSelected = false;
    }
    
    public void Lock()
    {
        image.color = Color.red;
        isLocked = true;
        GetComponent<Button>().interactable = false;
    }
    
    public void Unlock()
    {
        image.color = defaultColor;
        isLocked = false;
        GetComponent<Button>().interactable = true;
    }
}