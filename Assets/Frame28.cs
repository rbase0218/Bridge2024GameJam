using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Frame28 : MonoBehaviour
{
    private Button button;
    private bool isClicked;
    private int curIndex;
    private string name;
    
    private void Start()
    {
        button = GetComponentInChildren<Button>();
    }
    
    public void OnClickSelectButton()
    {
        name = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
    }

    public void OnClickSendButton()
    {
        var index = RoundManager.instance.GetNameToUserIndex(name);
        
        RoundManager.instance.voteList[index] += 1;
    }
}
