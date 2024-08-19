using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartProcess : UIWindow
{
    private enum Buttons
    {
        BackButton,
        ManualButton
    }
    private UISetDataBoard _dataBoard;
    private UINameInputBoard _inputBoard;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        _dataBoard = Utils.FindChild<UISetDataBoard>(gameObject, "Board - SetData", true);
        _dataBoard.onClickNextButton.AddListener(OpenNameInput);
        _dataBoard.Bind();
        
        _inputBoard = Utils.FindChild<UINameInputBoard>(gameObject, "Board - NameInput", true);
        _inputBoard.Bind();
        
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.BackButton).onClick.AddListener(OnClickBackButton);
        GetButton((int)Buttons.ManualButton).onClick.AddListener(OnClickManualButton);
        
        return true;
    }
    protected override bool EnterWindow()
    {
        OpenSetData();
        return true;
    }
    
    #region # [ OnClick Event ] #
    
    private void OnClickBackButton()
    {
        Managers.UI.CloseWindow();
    }
    
    private void OnClickManualButton()
    {
        Managers.UI.ShowWindow<UIManual>();
    }
    
    #endregion

    private void OpenNameInput(int userCount)
    {
        _dataBoard.gameObject.SetActive(false);
        
        _inputBoard.ShowInputField(userCount);
        _inputBoard.gameObject.SetActive(true);
    }

    private void OpenSetData()
    {
        _inputBoard.gameObject.SetActive(false);
        _dataBoard.gameObject.SetActive(true);
    }
}