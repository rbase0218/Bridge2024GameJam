using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(1);
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
        List<string> userNames = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            if (Get<TMP_InputField>(i).gameObject.activeSelf)
            {
                userNames.Add(Get<TMP_InputField>(i).text);
            }
        }
        Managers.Game.AddRangeUser(userNames);
    }
}