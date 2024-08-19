using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINameInputBoard : UIBase
{
    private enum InputFields
    {
        NameInputField_1,
        NameInputField_2,
        NameInputField_3,
        NameInputField_4,
        NameInputField_5,
        NameInputField_6
    }

    private enum Buttons
    {
        EntryButton
    }

    public void Bind()
    {
        Bind<TMP_InputField>(typeof(InputFields));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.EntryButton).onClick.AddListener(OnClickEntryButton);
    }

    private void OnClickEntryButton()
    {
        Save();
        
        // Scene 연결
    }
    
    public void ShowInputField(int count = 3)
    {
        for (int i = 0; i < 6; i++)
        {
            Get<TMP_InputField>(i).gameObject.SetActive(i < count);
        }
    }

    private void Save()
    {
        
    }
}