using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class UIInfo_Questions : UIScreen
{
    private enum Texts
    {
        AnswerText
    }

    private enum Buttons
    {
        BeforeButton_In,
        AfterButton_In
    }

    private enum Objects
    {
        Container
    }

    private int index;
    private List<GameObject> questionObjects = new List<GameObject>();
    private float objectWidth;
    private float padding;
    private bool isAnimating = false; // 애니메이션 중인지 확인하는 플래그
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        
        GetButton((int)Buttons.BeforeButton_In).onClick.AddListener(OnClickBeforeButton);
        GetButton((int)Buttons.AfterButton_In).onClick.AddListener(OnClickAfterButton);
        
        return true;
    }

    protected override bool EnterWindow()
    {
        CreateQuestionObjects();
        UpdateAnswerText();
        return true;
    }

    private void CreateQuestionObjects()
    {
        // 기존 오브젝트들 제거
        foreach (var obj in questionObjects)
        {
            if (obj != null)
                DestroyImmediate(obj);
        }
        questionObjects.Clear();

        // Managers.Game이 초기화되었는지 확인
        if (Managers.Game == null || Managers.Game.QuestionManager == null)
        {
            return;
        }

        // 실제 질문 로그 가져오기
        var questionLogs = Managers.Game.QuestionManager.GetAllQuestionLogs();
        if (questionLogs == null || questionLogs.Count == 0)
        {
            NoQuestion();
            return;
        }

        // 답변이 있는 질문만 필터링
        var answeredQuestions = questionLogs.Where(q => !string.IsNullOrEmpty(q.answer)).ToList();
        
        if (answeredQuestions.Count == 0)
        {
            NoQuestion();
            return;
        }

        var container = GetObject((int)Objects.Container);
        
        // Container 초기 위치 설정
        var containerRect = container.GetComponent<RectTransform>();
        containerRect.anchoredPosition = Vector2.zero;
        
        // 실제 QuestionObject 프리팹 사용
        var prefab = Managers.Data.QuestionObject;

        // 첫 번째 오브젝트 생성하여 크기 측정
        var firstObj = Instantiate(prefab, container.transform);
        var firstRect = firstObj.GetComponent<RectTransform>();
        objectWidth = firstRect.rect.width;
        padding = 250f; // 패딩 값 설정
        
        // 인덱스 초기화 (첫 번째 질문부터 시작)
        index = 0;

        // 답변이 있는 질문들만 오브젝트 생성
        for (int i = 0; i < answeredQuestions.Count; i++)
        {
            GameObject questionObj;
            if (i == 0)
            {
                questionObj = firstObj;
            }
            else
            {
                questionObj = Instantiate(prefab, container.transform);
            }

            // 위치 설정 (0번부터 일렬로 배치)
            var rect = questionObj.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(i * (objectWidth + padding), 0);

            // QuestionText 업데이트 (실제 질문 내용)
            var questionText = questionObj.GetComponentInChildren<TMP_Text>();
            if (questionText != null)
            {
                questionText.text = answeredQuestions[i].question;
            }

            questionObjects.Add(questionObj);
        }

        // 버튼 상태 업데이트
        UpdateButtonStates();
    }

    private void OnClickBeforeButton()
    {
        if (index <= 0 || isAnimating) return;

        Managers.Sound.PlaySFX("Click");
        
        // 애니메이션 시작
        isAnimating = true;
        SetButtonsInteractable(false);
        
        // Container를 오른쪽으로 부드럽게 이동
        float moveDistance = objectWidth + padding;
        
        var container = GetObject((int)Objects.Container);
        var containerRect = container.GetComponent<RectTransform>();
        var startPos = containerRect.localPosition;
        var endPos = new Vector3(startPos.x + moveDistance, startPos.y, startPos.z);
        
        // Container만 DOTween으로 애니메이션 (TimeScale 무시)
        var tween = containerRect.DOLocalMove(endPos, 0.3f)
            .SetEase(Ease.OutQuad)
            .SetUpdate(true); // TimeScale 무시하고 독립적으로 실행

        index--;
        UpdateButtonStates();
        
        // 애니메이션 완료 후 처리
        DOVirtual.DelayedCall(0.3f, () => {
            UpdateAnswerText();
            isAnimating = false;
            SetButtonsInteractable(true);
        }).SetUpdate(true);
    }

    private void OnClickAfterButton()
    {
        if (index >= questionObjects.Count - 1 || isAnimating) return;

        Managers.Sound.PlaySFX("Click");
        
        // 애니메이션 시작
        isAnimating = true;
        SetButtonsInteractable(false);
        
        // Container를 왼쪽으로 부드럽게 이동
        float moveDistance = objectWidth + padding;
        
        var container = GetObject((int)Objects.Container);
        var containerRect = container.GetComponent<RectTransform>();
        var startPos = containerRect.localPosition;
        var endPos = new Vector3(startPos.x - moveDistance, startPos.y, startPos.z);
        
        // Container만 DOTween으로 애니메이션 (TimeScale 무시)
        var tween = containerRect.DOLocalMove(endPos, 0.3f)
            .SetEase(Ease.OutQuad)
            .SetUpdate(true); // TimeScale 무시하고 독립적으로 실행

        index++;
        UpdateButtonStates();
        
        // 애니메이션 완료 후 처리
        DOVirtual.DelayedCall(0.3f, () => {
            UpdateAnswerText();
            isAnimating = false;
            SetButtonsInteractable(true);
        }).SetUpdate(true);
    }

    private void UpdateButtonStates()
    {
        SetButtonsInteractable(!isAnimating);
    }

    private void UpdateAnswerText()
    {
        if (questionObjects.Count == 0 || index < 0 || index >= questionObjects.Count) return;

        // Managers.Game이 초기화되었는지 확인
        if (Managers.Game == null || Managers.Game.QuestionManager == null)
        {
            return;
        }

        // 답변이 있는 질문들만 가져오기
        var questionLogs = Managers.Game.QuestionManager.GetAllQuestionLogs();
        var answeredQuestions = questionLogs.Where(q => !string.IsNullOrEmpty(q.answer)).ToList();
        
        if (answeredQuestions != null && index < answeredQuestions.Count)
        {
            GetText((int)Texts.AnswerText).text = answeredQuestions[index].answer;
        }
        else
        {
            GetText((int)Texts.AnswerText).text = "예/아니오";
        }   
    }

    public void RefreshData()
    {
        if (_init == false)
            Init();
        CreateQuestionObjects();
        UpdateAnswerText();
    }

    private void SetButtonsInteractable(bool interactable)
    {
        GetButton((int)Buttons.BeforeButton_In).interactable = interactable && index > 0;
        GetButton((int)Buttons.AfterButton_In).interactable = interactable && index < questionObjects.Count - 1;
    }

    private void NoQuestion()
    {
        var container = GetObject((int)Objects.Container);
        var containerRect = container.GetComponent<RectTransform>();
        containerRect.anchoredPosition = Vector2.zero;

        var prefab = Managers.Data.QuestionObject;
        var firstObj = Instantiate(prefab, container.transform);
        var firstRect = firstObj.GetComponent<RectTransform>();
        firstRect.anchoredPosition = Vector2.zero;

        var questionText = firstObj.GetComponentInChildren<TMP_Text>();
        if (questionText != null)
        {
            questionText.text = "질문 없음";
        }

        questionObjects.Add(firstObj);
        UpdateButtonStates();
        index = 0;
        SetButtonsInteractable(false);
        isAnimating = false;
    }
}