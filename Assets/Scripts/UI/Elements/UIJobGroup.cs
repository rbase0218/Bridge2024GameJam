using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIJobGroup : MonoBehaviour
{
    public TMP_Text text;

    public void SetText(string txt) => text.text = txt;
}