using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICardElement : MonoBehaviour
{
    public TMP_Text text;
    public GameObject _backObj;

    public void SetText(string txt) => text.text = txt;

    public void OpenCard()
    {
        _backObj.SetActive(false);
    }

    public void Reset()
    {
        _backObj.SetActive(true);
    }
    
    public bool GetCardActive() => _backObj.activeInHierarchy;
}
