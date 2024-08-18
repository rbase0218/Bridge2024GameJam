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

    private Button[] _buttons = new Button[6];
    private Button _selectButton;

    public UnityEvent<Button> onClickSubmitButton;

    // Init에 실행되지 않는 메서드
    public void Binding()
    {
        BindButton(typeof(Buttons));

        for (int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i] = Utils.FindChild(gameObject, $"Button" + (i + 1), true).GetComponent<Button>();
            _buttons[i].onClick.AddListener(() =>
            {
                _selectButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            });
        }
        
        GetButton((int)Buttons.SubmitButton).onClick.AddListener(() =>
        {
            if (_selectButton == null)
                return;
            onClickSubmitButton?.Invoke(_selectButton);
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
    }
}
