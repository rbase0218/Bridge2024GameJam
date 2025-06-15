using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CorrectEffect : MonoBehaviour
{
    [Header("콘")]
    public Image coneImage;
    [Header("콘페티")]
    public Image confettiImage;
    private Button coneButton;

    void Start()
    {
        confettiImage.color = new Color(1, 1, 1, 0);
        confettiImage.rectTransform.localScale = Vector3.zero;
        
        coneButton = coneImage.GetComponent<Button>();
        if (coneButton != null)
            coneButton.onClick.AddListener(PlayConfettiEffect);
    }

    public void PlayConfettiEffect()
    {
        if (coneButton != null)
            coneButton.interactable = false;

        // 콘 흔들기 (Y축으로 위아래로 2회)
        RectTransform coneRect = coneImage.rectTransform;
        Sequence coneSeq = DOTween.Sequence();
        coneSeq.Append(coneRect.DOLocalMoveY(10f, 0.18f).SetRelative())
            .Append(coneRect.DOLocalMoveY(-20f, 0.36f).SetRelative())
            .Append(coneRect.DOLocalMoveY(10f, 0.18f).SetRelative())
            .AppendInterval(0.2f)
            .AppendCallback(PlayConfettiParticleEffect);
    }

    private void PlayConfettiParticleEffect()
    {
        RectTransform confettiRect = confettiImage.rectTransform;
        Sequence confettiSeq = DOTween.Sequence();
        confettiSeq.AppendCallback(() => {
                confettiRect.localScale = Vector3.zero ;
                confettiImage.color = new Color(1, 1, 1, 0);
                confettiImage.gameObject.SetActive(true);
            })
            // 스케일 업 + 알파 업
            .Append(confettiRect.DOScale(1.2f, 0.18f).SetEase(Ease.OutBack))
            .Join(confettiImage.DOFade(1f, 0.15f))
            // 천천히 아래로 이동하며 페이드아웃
            .Append(confettiRect.DOLocalMoveY(-80f, 0.8f).SetRelative().SetEase(Ease.InQuad))
            .Join(confettiImage.DOFade(0f, 1.2f))
            .AppendCallback(() => {
                confettiRect.localScale = Vector3.zero;
                confettiImage.gameObject.SetActive(false);
            });
    }
}
