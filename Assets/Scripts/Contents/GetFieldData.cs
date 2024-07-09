using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetFieldData : MonoBehaviour
{
    private void Update()
    {
        if (RoundManager.instance == null)
            return;

        RoundManager.instance.inputWord = GetComponent<TMP_InputField>().text;
    }
}
