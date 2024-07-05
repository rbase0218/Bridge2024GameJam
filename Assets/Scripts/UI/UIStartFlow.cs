using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStartFlow : UIWindow
{
    private enum Buttons
    {
        NextButton,
        //ExitButton
    }

    private enum Swipes
    {
        CountSwipe
    }

    private enum Dropdowns
    {
        CategoryDropdown
    }

    private int categoryIndex = 0;

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        Bind<UISwipe>(typeof(Swipes));
        Bind<TMP_Dropdown>(typeof(Dropdowns));
        
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);

        for (int i = 0; i < Managers.Data.categoryArray.Length; ++i)
        {
            var option = new TMP_Dropdown.OptionData();
            option.text = Managers.Data.categoryArray[i];
            
            Get<TMP_Dropdown>((int)Dropdowns.CategoryDropdown).options.Add(option);
        }
        
        Get<TMP_Dropdown>((int)Dropdowns.CategoryDropdown).SetValueWithoutNotify(-1);
        Get<TMP_Dropdown>((int)Dropdowns.CategoryDropdown).onValueChanged.AddListener(SetCategoryIndex);
        return true;
    }

    private void OnClickNextButton()
    {
        Debug.Log("Click - Next Button");
        
        // Current UI를 닫는다.
        Managers.UI.CloseWindow();
        // NameSelect 창을 열고 Instance를 보유한다.
        var nameSelect = Managers.UI.ShowWindow<UINameSelect>();
        var stringCount = Get<UISwipe>((int)Swipes.CountSwipe).GetData();
        var personCount = Convert.ToInt32(stringCount);
        
        // NameSelect 창에서 MakeChildren을 통해서 Child를 생성한다.
        nameSelect.ExecuteProcess(personCount);
        Managers.Game.currCategoryIndex = categoryIndex;
    }

    private void SetCategoryIndex(int index)
    {
        categoryIndex = index;
    }
}
