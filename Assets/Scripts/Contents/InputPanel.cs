using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InputPanel : MonoBehaviour
{
    private List<TMP_Text> texts;
    
    // Start is called before the first frame update
    void OnValidate()
    {
        texts = GameObject.FindGameObjectsWithTag("NameTag")
            .Select(x => x.GetComponent<TMP_Text>())
            .ToList();
    }

    void Start()
    {
        
    }
    
    public void SetText(string text)
    {
        texts[0].text = text;
    }
}
