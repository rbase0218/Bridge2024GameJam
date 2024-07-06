using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputWindow : MonoBehaviour
{
    [SerializeField] private AnswerTimerLayer answerTimerLayer;
    
    TMP_InputField inputField;
    
    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(InputText);
    }

    private void InputText(string text)
    {
        answerTimerLayer.SaveAnswer(text);
    }
}