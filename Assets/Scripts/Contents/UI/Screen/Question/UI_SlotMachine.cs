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

    private const float ITEM = 250f;  // 슬롯 셀 높이 (250x250 사양)
    private const int SPINS = 4;      // 정확히 4바퀴

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
        
        GetText((int)Texts.NameText).text = Managers.Game.GetCurrentPlayer().userName;

        _maxCount = Mathf.Clamp(Managers.Game.QuestionManager.GetLogCount(), 1, 5);

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

    private IEnumerator SpinToIndexCoroutine(int targetIndex)
    {
        var rt = _slotObject.GetComponent<RectTransform>();

        // 현재 시작 위치를 0~H로 정규화
        float H = ITEM * _maxCount;
        float startYRaw = rt.anchoredPosition.y;
        float startY = Mathf.Repeat(startYRaw, H);

        // 목표 절대 이동량: 4바퀴 + 타겟칸 오프셋
        float targetYAbs = startY + (SPINS * H) + (targetIndex * ITEM);

        // 총 소요 시간(가감속 포함). 필요 시 취향대로 조절 가능.
        float duration = 2.5f + 0.2f * _maxCount; // 개수 많을수록 약간 더 길게
        float t = 0f;

        // 회전 중 입력/버튼 방지
        var nextBtn = GetButton((int)Buttons.NextButton);
        if (nextBtn) nextBtn.interactable = false;

        while (t < duration)
        {
            t += Time.deltaTime;
            float u = Mathf.Clamp01(t / duration);

            // Cubic ease-out (부드럽게 감속)
            float eased = EaseOutCubic(u);

            float currentYAbs = Mathf.LerpUnclamped(startY, targetYAbs, eased);

            // 보여줄 위치는 래핑하여 자연스러운 무한 스크롤
            float wrappedY = Mathf.Repeat(currentYAbs, H);
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, wrappedY);

            yield return null;
        }

        // 도착 시 정확히 타겟 칸에 스냅
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, targetIndex * ITEM);

        if (nextBtn) nextBtn.interactable = true;

        _slotCoroutine = null;
    }

    private static float EaseOutCubic(float x)
    {
        // 1 - (1 - x)^3
        float a = 1f - x;
        return 1f - (a * a * a);
    }
}
