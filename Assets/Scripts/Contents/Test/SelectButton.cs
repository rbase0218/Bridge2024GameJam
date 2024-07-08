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

    private void Start()
    {
        textTMP = transform.GetChild(0).GetComponent<TMP_Text>();
        image = GetComponent<Image>();
        image.color = Color.blue;
        defaultColor = image.color;
    }

    public void SetText(string text)
    {
        if (textTMP == null)
            textTMP = transform.GetChild(0).GetComponent<TMP_Text>();

        textTMP.text = text;
    }

    public void Select()
    {
        if (image == null)
            image = GetComponent<Image>();

        image.color = Color.blue;
        isSelected = true;
    }

    public void Reset()
    {
        if (image == null)
            image = GetComponent<Image>();

        image.color = Color.blue;
        
        GetComponent<Button>().interactable = true;
        isSelected = false;
    }

    public void UnSelect()
    {
        if (isLocked)
            return;

        if (image == null)
            image = GetComponent<Image>();

        image.color = Color.gray;
        isSelected = false;
    }

    public void Lock()
    {
        if (image == null)
            image = GetComponent<Image>();
        image.color = Color.red;

        isLocked = true;
        GetComponent<Button>().interactable = false;
    }

    public void Unlock()
    {
        if (image == null)
            image = GetComponent<Image>();

        image.color = defaultColor;
        isLocked = false;
        GetComponent<Button>().interactable = true;
    }
}