using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINameField : MonoBehaviour
{
    public List<TMP_Text> GetFields()
    {
        var resultFields = new List<TMP_Text>();
        
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                var text = transform.GetChild(i).GetComponentInChildren<TMP_Text>();
                resultFields.Add(text);
            }
        }
        return resultFields;
    }
}
