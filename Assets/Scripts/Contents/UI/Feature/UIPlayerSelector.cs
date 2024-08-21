using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlayerSelector : UIBase
{
    private enum Buttons
    {
        SubmitButton
    }

    private enum Texts
    {
        InfoText
    }

    private enum Layouts
    {
        SelectButtons
    }

    private Button[] _buttons = new Button[6];
    private string _selectButtonText;

    public UnityEvent<string> onClickSubmitButton;

    // Init에 실행되지 않는 메서드
    public void Binding()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        Bind<VerticalLayoutGroup>(typeof(Layouts));
        
        for (int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i] = Utils.FindChild(gameObject, $"Button" + (i + 1), true).GetComponent<Button>();
            _buttons[i].onClick.AddListener(() =>
            {
                _selectButtonText = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().GetComponentInChildren<TMP_Text>().text;
            });
        }
        
        GetButton((int)Buttons.SubmitButton).onClick.AddListener(() =>
        {
            if (_selectButtonText == string.Empty)
                return;
            onClickSubmitButton?.Invoke(_selectButtonText);
        });
    }

    public void ShowButton(params string[] names)
    {
        for (int i = 0; i < _buttons.Length; ++i)
        {
            if (i < names.Length)
            {
                _buttons[i].gameObject.SetActive(true);
                _buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = names[i];
            }
            else
                _buttons[i].gameObject.SetActive(false);
        }

        float spacingValue = .0f;
        switch (names.Length)
        {
            case 2:
            case 3:
                spacingValue = 101f;
                break;
            case 4:
                spacingValue = 71f;
                break;
            case 5:
                spacingValue = 40f;
                break;
        }
        Get<VerticalLayoutGroup>((int)Layouts.SelectButtons).spacing = spacingValue;
    }
}
