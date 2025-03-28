using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    private enum Texts
    {
        DuplicationErrorText
    }

    public void Bind()
    {
        Bind<TMP_InputField>(typeof(InputFields));
        Bind<TMP_Text>(typeof(Texts));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.EntryButton).onClick.AddListener(OnClickEntryButton);
    }

    private void OnClickEntryButton()
    {
        if(Save())
            SceneManager.LoadScene(1);
        return;
    }
    
    public void ShowInputField(int count = 3)
    {
        for (int i = 0; i < 6; i++)
        {
            Get<TMP_InputField>(i).gameObject.SetActive(i < count);
        }
    }
    
    private void ShowDuplicationErrorText()
    {
        Get<TMP_Text>((int)Texts.DuplicationErrorText).gameObject.SetActive(true);
    }

    private bool Save()
    {
        List<string> userNames = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            if (Get<TMP_InputField>(i).gameObject.activeSelf)
            {
                userNames.Add(Get<TMP_InputField>(i).text);
            }
        }

        if (userNames.Contains(""))
            return false;

        if (userNames.Count() != userNames.Distinct().Count())
        {
            ShowDuplicationErrorText();
            return false;
        }
        
        Managers.Game.AddRangeUser(userNames);
        return true;
    }
}