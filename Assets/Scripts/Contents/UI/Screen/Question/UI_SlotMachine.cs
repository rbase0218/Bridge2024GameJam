using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotMachine : UIScreen
{
    private enum Texts
    {
        NameText
    }
    private enum Buttons
    {
        NextButton
    }

    private enum GameObjects
    {
        SlotObject,
    }

    private GameObject _slotObject;
    private int _maxCount = 0;        // 1~5

    private Coroutine _slotCoroutine;
    private int _resultIndex = 0;     // 0-based

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }

    protected override bool EnterWindow()
    {
        onOpen?.RemoveAllListeners();
        
        // 이름을 변경한다.
        GetText((int)Texts.NameText).text = Managers.Game.GetCurrentPlayer().userName;

        // 질문의 최대 개수를 정한다.
        _maxCount = Managers.Game.QuestionManager.GetLogCount();

        // Random.Range(int,int) 의 상한은 "제외"이므로 +1 필요
        int resultNumber = Random.Range(1, _maxCount + 1); // 1.._maxCount
        _resultIndex = resultNumber - 1;                   // 0.._maxCount-1

        // 위치 초기화 (UI는 RectTransform.anchoredPosition 권장)
        _slotObject = GetObject((int)GameObjects.SlotObject);
        var rt = _slotObject.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 0f);

        // 결과 인덱스를 미리 고정 (게임 로직)
        Managers.Game.QuestionManager.SelectRandQuestion(resultNumber);
        Debug.Log($"질문 개수(선택 인덱스 1-based) : {resultNumber}");

        onOpen?.AddListener(() =>
        {
            _slotCoroutine = StartCoroutine(SpinToIndexCoroutine(_resultIndex));
        });

        return true;
    }

    private void OnClickNextButton()
    {
        if (_slotCoroutine != null)
            return; // 도는 중엔 버튼 막기

        OnNextScreen<UI_TextConfirm01>();
    }

    // 설정값(원하면 인스펙터로 빼도 됩니다)
    const float ITEM   = 250f;   // 슬롯 하나의 높이
    const int   COUNT  = 5;      // 슬롯 개수
    const float SPINS  = 3f;     // 멈추기 전에 도는 "완전 회전" 수
    const float DURATION = 2.2f; // 전체 회전 시간(가감속 포함)

// targetIndex는 0~COUNT-1 기준입니다.
// 만약 1~COUNT 로 전달된다면, 아래 첫 줄을 해제하세요.
// targetIndex = ((targetIndex - 1) % COUNT + COUNT) % COUNT;

    private IEnumerator SpinToIndexCoroutine(int targetIndex)
    {
        var rt = _slotObject.GetComponent<RectTransform>();
        var nextBtn = GetButton((int)Buttons.NextButton);
        if (nextBtn) nextBtn.interactable = false;

        // ---- 준비: 현재 위치 정규화/목표 위치 계산 ----
        float H = ITEM * COUNT;                                // 한 바퀴(전체 높이)
        float startY = Mathf.Repeat(rt.anchoredPosition.y, H); // 0~H 범위로 정규화
        targetIndex = ((targetIndex % COUNT) + COUNT) % COUNT; // 안전 클램프

        // 여러 바퀴 돈 뒤 목표 인덱스 위치까지의 "절대" 타겟
        float targetYAbs = startY + (SPINS * H) + (targetIndex * ITEM);

        // ---- 애니메이션(가속-감속: easeOutCubic) ----
        float t = 0f;
        Vector2 basePos = rt.anchoredPosition;

        while (t < DURATION)
        {
            t += Time.deltaTime;
            float u = Mathf.Clamp01(t / DURATION);

            // easeOutCubic: 1 - (1 - u)^3
            float eased = 1f - Mathf.Pow(1f - u, 3f);

            float y = Mathf.Lerp(startY, targetYAbs, eased);
            // 화면상 래핑
            float yWrapped = Mathf.Repeat(y, H);

            rt.anchoredPosition = new Vector2(basePos.x, yWrapped);
            yield return null;
        }

        // ---- 스냅 정렬(미세 오차 제거) ----
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, targetIndex * ITEM);

        if (nextBtn) nextBtn.interactable = true;
        _slotCoroutine = null;
    }

}
