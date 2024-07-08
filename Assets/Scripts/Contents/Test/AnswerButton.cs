using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerSelect : MonoBehaviour
{
    private TMP_Text textTMP;
    private Image image;
    private Color defaultColor;
    private bool isSelected;
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
    
    public void Select()
    {
        image.color = Color.blue;
        isSelected = true;
    }
    
    public void Reset()
    {
        image.color = Color.blue;;
        isSelected = false;
    }
    
    public void UnSelect()
    {
        image.color = Color.gray;
        isSelected = false;
    }
}
