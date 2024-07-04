using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InputLayer : MonoBehaviour, ILayoutControl
{
    private List<TMP_Text> texts;
    
    void OnValidate()
    {
        texts = GameObject.FindGameObjectsWithTag("NameTag")
            .Select(x => x.GetComponent<TMP_Text>())
            .ToList();
    }
    
    public void SetText(string text)
    {
        texts[0].text = text;
    }

    public void ExitLayout()
    {
        gameObject.SetActive(false);
    }

    public void StartLayout()
    {
        gameObject.SetActive(true);
    }
}