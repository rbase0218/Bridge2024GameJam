using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class NextOrderLayer : MonoBehaviour, ILayoutControl
{
    public static Action OnExitLayout;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text nameText2;

    [SerializeField] private NamePanel namePanel;
    [SerializeField] private NamePanel targetNamePanel;
    [SerializeField] private GameObject imageButton;
    
    private float timer;
    private bool isTimerRunning;
    private UserInfo curUser;

    private Vector2 nameboxSizeDelta;
    private Vector3 nameboxLocalPosition;
    private Vector3 namePanelLocalPosition;
    private Vector3 thisTimeTextLocalPosition;
    private Vector3 orderTextLocalPosition;
    private float nameTextFontSize;
    private float thisTimeTextFontSize;
    private float orderTextFontSize;

    private void Start()
    {
        nameboxSizeDelta = namePanel.nameBox.GetComponent<RectTransform>().sizeDelta;
        nameboxLocalPosition = namePanel.nameBox.GetComponent<RectTransform>().localPosition;
        
        namePanelLocalPosition = namePanel.GetComponent<RectTransform>().localPosition;
        nameTextFontSize = nameText.fontSize;
        
        thisTimeTextLocalPosition = namePanel.thisTimeText.GetComponent<RectTransform>().localPosition;
        thisTimeTextFontSize = namePanel.thisTimeText.fontSize;
        
        orderTextLocalPosition = namePanel.orderText.GetComponent<RectTransform>().localPosition;
        orderTextFontSize = namePanel.orderText.fontSize;
    }

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
        Reset();
        timer = 0f;
        isTimerRunning = false;
        imageButton.SetActive(true);
        gameObject.SetActive(false);
        curUser.myTurn = false;
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        this.curUser = curUser;
        this.curUser.myTurn = true;
        nameText.text = curUser.name;
        gameObject.SetActive(true);
    }

    public void StartAnimation()
    {
        imageButton.SetActive(false);
        NameBoxScaleAnimation();
        ThisTimeTextScaleAnimation();
        OrderTextScaleAnimation();
        timer = 0f;
        isTimerRunning = true;
    }

    private void Reset()
    {
        namePanel.nameBox.GetComponent<RectTransform>().sizeDelta =nameboxSizeDelta;
        namePanel.nameBox.GetComponent<RectTransform>().localPosition = nameboxLocalPosition;
        
        namePanel.GetComponent<RectTransform>().localPosition = namePanelLocalPosition;
        nameText.fontSize =nameTextFontSize;
        
        namePanel.thisTimeText.GetComponent<RectTransform>().localPosition = thisTimeTextLocalPosition;
        namePanel.thisTimeText.fontSize = thisTimeTextFontSize;
        
        namePanel.orderText.GetComponent<RectTransform>().localPosition = orderTextLocalPosition;
        namePanel.orderText.fontSize = orderTextFontSize;

        namePanel.nameBox.GetComponent<Image>().DOFade(1, 0);
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
            .Join(DOTween.To(() => nameText.fontSize, x => nameText.fontSize = x, nameText2.fontSize, 0.5f))
            .Join(namePanel.nameBox.GetComponent<Image>().DOFade(0, 0.5f));


        // 목표 크기로 줄어들기
    }

    private void ThisTimeTextScaleAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(namePanel.thisTimeText.GetComponent<RectTransform>()
                .DOLocalMove(targetNamePanel.thisTimeText.GetComponent<RectTransform>().localPosition, 0.5f))
            .Join(DOTween.To(() => namePanel.thisTimeText.fontSize, x => namePanel.thisTimeText.fontSize = x,
                targetNamePanel.thisTimeText.fontSize, 0.5f));
    }

    private void OrderTextScaleAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(namePanel.orderText.GetComponent<RectTransform>()
                .DOLocalMove(targetNamePanel.orderText.GetComponent<RectTransform>().localPosition, 0.5f))
            .Join(DOTween.To(() => namePanel.orderText.fontSize, x => namePanel.orderText.fontSize = x,
                targetNamePanel.orderText.fontSize, 0.5f));
    }
}