using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINameFields : UIBase
{
    private List<TMP_InputField> _inputFields;
    private VerticalLayoutGroup _verticalLayoutGroup;

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        _verticalLayoutGroup = Utils.TryOrAddComponent<VerticalLayoutGroup>(this.gameObject);
        _inputFields = new List<TMP_InputField>();
        
        for (int i = 0; i < transform.childCount; ++i)
        {
            _inputFields.Add(transform.GetChild(i).GetComponent<TMP_InputField>());
        }

        return true;
    }

    public void SetField(int count)
    {
        // 얼마나 Open해야 하는가?
        for (int i = 0; i < count; ++i)
        {
            _inputFields[i].gameObject.SetActive(true);
        }
        
        // Spacing Value 잡기
        float resultSpacing = .0f;

        if (count == 3)
            resultSpacing = 101;
        else if (count == 4)
            resultSpacing = 71;
        else if (count == 5)
            resultSpacing = 40;
        else if (count == 6)
            resultSpacing = 27;
        
        _verticalLayoutGroup.spacing = resultSpacing;
    }

    public void HideAll()
    {
        for (int i = 0; i < _inputFields.Count; ++i)
        {
            _inputFields[i].gameObject.SetActive(false);
        }
    }

    public List<TMP_InputField> GetFields()
    {
        return _inputFields;
    }
}
