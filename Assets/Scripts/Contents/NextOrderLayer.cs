using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;

public class NextOrderLayer : MonoBehaviour, ILayoutControl
{
    public static Action OnExitLayout;
    
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text nameText2;

    [SerializeField] private NamePanel namePanel;
    [SerializeField] private NamePanel targetNamePanel;

    private float timer;
    private bool isTimerRunning;
    
    private void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                // 3초가 지났으므로 액션 호출
                OnExitLayout?.Invoke();

                // 타이머 중지
                isTimerRunning = false;
            }
        }
    }
    
    public void ExitLayout()
    {
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        nameText.text = curUser.name;
        gameObject.SetActive(true);
    }
    
    public void StartAnimation()
    {
        NameBoxScaleAnimation();
        ThisTimeTextScaleAnimation();
        OrderTextScaleAnimation();
        timer = 0f;
        isTimerRunning = true;
    }

    private void NameBoxScaleAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(namePanel.nameBox.GetComponent<RectTransform>()
                .DOSizeDelta(targetNamePanel.nameBox.GetComponent<RectTransform>().rect.size, 0.5f))
            .Join(namePanel.nameBox.GetComponent<RectTransform>()
                .DOLocalMove(targetNamePanel.nameBox.GetComponent<RectTransform>().localPosition, 0.5f))
            .Join(
                namePanel.GetComponent<RectTransform>()
                    .DOLocalMove(targetNamePanel.GetComponent<RectTransform>().localPosition, 0.5f))
            .Join(DOTween.To(() => nameText.fontSize, x => nameText.fontSize = x, nameText2.fontSize, 0.5f));
        

        // 목표 크기로 줄어들기
    }

    private void ThisTimeTextScaleAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(namePanel.thisTimeText.GetComponent<RectTransform>()
                .DOLocalMove(targetNamePanel.thisTimeText.GetComponent<RectTransform>().localPosition, 0.5f))
            .Join(DOTween.To(() => namePanel.thisTimeText.fontSize, x => namePanel.thisTimeText.fontSize = x, targetNamePanel.thisTimeText.fontSize, 0.5f));
    }
    
    private void OrderTextScaleAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(namePanel.orderText.GetComponent<RectTransform>()
                .DOLocalMove(targetNamePanel.orderText.GetComponent<RectTransform>().localPosition, 0.5f))
            .Join(DOTween.To(() => namePanel.orderText.fontSize, x => namePanel.orderText.fontSize = x, targetNamePanel.orderText.fontSize, 0.5f));
    }
}