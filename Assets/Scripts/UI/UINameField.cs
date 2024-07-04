using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINameField : MonoBehaviour
{
    public List<TMP_Text> _inputFields = new List<TMP_Text>(); 
        
    public void RefreshField()
    {
        _inputFields.Clear();
        
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                var text = transform.GetChild(i).GetComponentInChildren<TMP_Text>();
                _inputFields.Add(text);
            }
        }
    }
}
