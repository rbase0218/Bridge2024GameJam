using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    private Sequence sequence;
    private Image image;
    private RectTransform rect;

    private void Awake()
    {
        TryGetComponent(out image);
        TryGetComponent(out rect);
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
            .Append(image.DOFade(0, 0f))
            .Append(image.DOFade(1, 0.5f))
            .Join(rect.DOAnchorPosY(10f, 0.5f));
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
