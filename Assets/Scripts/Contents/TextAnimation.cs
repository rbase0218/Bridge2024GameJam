using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Sequence = DG.Tweening.Sequence;

public class TextAnimation : MonoBehaviour
{
    private Sequence sequence;
    private TMP_Text text;
    private RectTransform rectTransform;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
    }
    
    private void OnEnable()
    {
        FadeInAnimation();
    }

    private void ScaleAnimation()
    {
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.2f, 0.5f));
        sequence.Append(transform.DOScale(1f, 0.5f));
        sequence.SetLoops(-1);
    }

    private void FadeInAnimation()
    {
        sequence = DOTween.Sequence();
        sequence
            .Append(text.DOFade(0, 0f))
            .Append(text.DOFade(1, 0.5f))
            .Join(rectTransform.DOAnchorPosY(10f, 0.5f));
    }

    private void OnDisable()
    {
        sequence.Kill();
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
    
    public void StopAnimation()
    {
        sequence.Kill();
    }
    
    public void StartAnimation()
    {
        sequence.Restart();
    }
}
