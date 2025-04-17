using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISetDataBoard : UIBase
{
    private enum Dropdowns
    {
        CategoryDropdown
    }

    private enum Buttons
    {
        AfterButton,
        BeforeButton,
        AddCategoryButton,
        NextButton
    }

    private enum Texts
    {
        SwipeValue
    }

    private enum Objects
    {
        SpyIconGroup,
        NobleIconGroup,
        ActorIconGroup,
    }

    private int _userCount = 4;

    public UnityEvent<int> onClickNextButton;
    
    private int _selectCategoryIndex = 0;
    
    public void Bind()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        
        Bind<TMP_Dropdown>(typeof(Dropdowns));
        
        GetButton((int)Buttons.AfterButton).onClick.AddListener(OnClickAfterButton);
        GetButton((int)Buttons.BeforeButton).onClick.AddListener(OnClickBeforeButton);
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        var dropdown = Get<TMP_Dropdown>((int)Dropdowns.CategoryDropdown);
        dropdown.AddOptions(Managers.Data.categoryArray.ToList());
        dropdown.onValueChanged.AddListener((x) =>
        {
            Managers.Sound.PlaySFX("Click");
            _selectCategoryIndex = x;
        } );
    }
    
    private void OnClickAfterButton()
    {
        Managers.Sound.PlaySFX("Click");

        if (_userCount >= 6)
            return;
        
        _userCount++;
        
        RefreshUI();
    }
    
    private void OnClickBeforeButton()
    {
        Managers.Sound.PlaySFX("Click");

        if (_userCount <= 4)
            return;
        
        _userCount--;
        
        RefreshUI();
    }

    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");

        Managers.Game.PickGameTopic(_selectCategoryIndex);
        onClickNextButton?.Invoke(_userCount);
    }
    
    #region # [ Update - UI ] #

    private void RefreshUI()
    {
        UpdateUserCountUI();
        UpdateJobIconUI();
    }

    private void UpdateUserCountUI()
    {
        GetText((int)Texts.SwipeValue).SetText(_userCount.ToString());
    }

    private void UpdateJobIconUI()
    {
        GetObject((int)Objects.ActorIconGroup).SetActive(_userCount >= 5);
    }
    
    #endregion
}