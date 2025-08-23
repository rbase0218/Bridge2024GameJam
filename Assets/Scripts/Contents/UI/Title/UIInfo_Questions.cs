using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class UIInfo_Questions : UIWindow
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
        QuestionContainer
    }

    private int index;
    private List<GameObject> questionObjects = new List<GameObject>();
    private float objectWidth;
    private float padding;
    
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
            Debug.LogWarning("GameManager or QuestionManager is not initialized yet.");
            return;
        }

        // 실제 질문 로그 가져오기
        var questionLogs = Managers.Game.QuestionManager.GetAllQuestionLogs();
        if (questionLogs == null || questionLogs.Count == 0)
        {
            Debug.LogWarning("No question logs found. Make sure temporary data is added.");
            return;
        }

        var container = GetObject((int)Objects.QuestionContainer);
        
        // 실제 QuestionObject 프리팹 사용
        var prefab = Managers.Data.QuestionObject;
        if (prefab == null)
        {
            Debug.LogError("QuestionObject prefab is null. Please check DataManager initialization.");
            return;
        }

        // 첫 번째 오브젝트 생성하여 크기 측정
        var firstObj = Instantiate(prefab, container.transform);
        var firstRect = firstObj.GetComponent<RectTransform>();
        objectWidth = firstRect.rect.width;
        padding = 50f; // 패딩 값 설정
        
        // 인덱스 초기화 (첫 번째 질문부터 시작)
        index = 0;

        // 모든 질문 오브젝트 생성
        for (int i = 0; i < questionLogs.Count; i++)
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
                questionText.text = questionLogs[i].question;
            }

            questionObjects.Add(questionObj);
        }

        // 버튼 상태 업데이트
        UpdateButtonStates();
    }

    private void OnClickBeforeButton()
    {
        if (index <= 0) return;

        Managers.Sound.PlaySFX("Click");
        
        // 모든 오브젝트를 오른쪽으로 이동
        float moveDistance = objectWidth + padding;
        foreach (var obj in questionObjects)
        {
            var rect = obj.GetComponent<RectTransform>();
            rect.DOAnchorPosX(rect.anchoredPosition.x + moveDistance, 0.5f);
        }

        index--;
        UpdateButtonStates();
        
        // 애니메이션 완료 후 AnswerText 업데이트
        DOVirtual.DelayedCall(0.5f, UpdateAnswerText);
    }

    private void OnClickAfterButton()
    {
        if (index >= questionObjects.Count - 1) return;

        Managers.Sound.PlaySFX("Click");
        
        // 모든 오브젝트를 왼쪽으로 이동
        float moveDistance = objectWidth + padding;
        foreach (var obj in questionObjects)
        {
            var rect = obj.GetComponent<RectTransform>();
            rect.DOAnchorPosX(rect.anchoredPosition.x - moveDistance, 0.5f);
        }

        index++;
        UpdateButtonStates();
        
        // 애니메이션 완료 후 AnswerText 업데이트
        DOVirtual.DelayedCall(0.5f, UpdateAnswerText);
    }

    private void UpdateButtonStates()
    {
        // 첫 번째면 Before 버튼 비활성화
        GetButton((int)Buttons.BeforeButton_In).interactable = index > 0;
        
        // 마지막이면 After 버튼 비활성화
        GetButton((int)Buttons.AfterButton_In).interactable = index < questionObjects.Count - 1;
    }

    private void UpdateAnswerText()
    {
        if (questionObjects.Count == 0 || index < 0 || index >= questionObjects.Count) return;

        // Managers.Game이 초기화되었는지 확인
        if (Managers.Game == null || Managers.Game.QuestionManager == null)
        {
            Debug.LogWarning("GameManager or QuestionManager is not initialized yet.");
            return;
        }

        // 실제 질문 로그에서 현재 인덱스의 답변을 가져와서 설정
        var questionLogs = Managers.Game.QuestionManager.GetAllQuestionLogs();
        if (questionLogs != null && index < questionLogs.Count)
        {
            GetText((int)Texts.AnswerText).text = questionLogs[index].answer;
        }
        else
        {
            GetText((int)Texts.AnswerText).text = "답변을 불러올 수 없습니다.";
        }
    }
}