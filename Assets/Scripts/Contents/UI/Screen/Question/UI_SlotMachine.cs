using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotMachine : UIScreen
{
    private enum Buttons
    {
        NextButton
    }

    private enum GameObjects
    {
        SlotObject,
    }

    private GameObject _slotObject;
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));
        
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        return true;
    }

    protected override bool EnterWindow()
    {
        onOpen?.RemoveAllListeners();
        
        var maxCount = Managers.Game.GetQuestionLogCount();
        var resultNumber = Random.Range(1, maxCount);

        // 위치 초기화
        _slotObject = GetObject((int)GameObjects.SlotObject);
        _slotObject.transform.localPosition = Vector3.zero;
        
        // 랜덤 수를 미리 정함.
        Managers.Game.SetRandQuestionCount(resultNumber);
        // 이후로 SlotMachine을 만들면 됨.
        
        onOpen?.AddListener(() => StartCoroutine(StartSlotCoroutine()));
        
        return true;
    }

    private void OnClickNextButton()
    {
        OnNextScreen<UI_TextConfirm01>();
    }

    private IEnumerator StartSlotCoroutine()
    {
        Debug.Log("Coroutine 동작");
        for (int i = 0; i < (20 * 8) + (20 * 1) ; ++i)
        {
            _slotObject.transform.localPosition += Vector3.up * 31.25f;
            if (_slotObject.transform.localPosition.y > 1250f)
            {
                _slotObject.transform.localPosition = Vector3.zero;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    
}
