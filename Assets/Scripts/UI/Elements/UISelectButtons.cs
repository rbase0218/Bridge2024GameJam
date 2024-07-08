using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelectButtons : MonoBehaviour
{
    public List<GameObject> selectButtonList;
    public VerticalLayoutGroup _layoutGroup;

    public void SetData(params string[] buttonData)
    {
        SetSpacing(buttonData.Length);
        
        for (int i = 0; i < buttonData.Length; ++i)
        {
            selectButtonList[i].GetComponentInChildren<TMP_Text>().text = buttonData[i];
        }
    }

    private void SetSpacing(int count)
    {
        switch (count)
        {
            case 3:
                _layoutGroup.spacing = 101f;
                break;
            case 4:
                _layoutGroup.spacing = 71f;
                break;
            case 5:
                _layoutGroup.spacing = 40f;
                break;
            case 6:
                _layoutGroup.spacing = 27f;
                break;
            default:
                return;
        }
    }
}
