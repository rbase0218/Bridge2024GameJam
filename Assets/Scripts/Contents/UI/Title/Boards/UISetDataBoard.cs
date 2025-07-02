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
        
        // Dropdown 컴포넌트를 가져온다.
        var dropdown = Get<TMP_Dropdown>((int)Dropdowns.CategoryDropdown);
        // 옵션 추가
        dropdown.AddOptions(Managers.Data.categoryArray.ToList());
        
        // 옵션이 변경된다면 사운드 발생
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

        // 주제를 랜덤으로 선정한다.
        Managers.Game.PickTopic(_selectCategoryIndex);
        
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