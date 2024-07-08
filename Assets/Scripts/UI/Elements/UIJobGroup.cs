using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIJobGroup : MonoBehaviour
{
    public TMP_Text text;
    public TMP_Text second;

    public void SetText(string txt) => text.text = txt;
    public void SetSecond(string txt) => second.text = txt;
}
