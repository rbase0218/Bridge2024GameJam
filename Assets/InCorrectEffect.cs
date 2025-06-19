using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InCorrectEffect : MonoBehaviour
{
    public Image bloodImage1;
    public Image bloodImage2;

    void Start()
    {
        // 마스크는 처음에 비활성화
        bloodImage1.gameObject.SetActive(false);
        bloodImage2.gameObject.SetActive(false);

        // 마스크는 그대로 두고, 자식(0번째) Image만 애니메이션
        Image realBlood1 = bloodImage1.transform.GetChild(0).GetComponent<Image>();
        Image realBlood2 = bloodImage2.transform.GetChild(0).GetComponent<Image>();

        // 피벗을 중앙으로 강제 설정
        realBlood1.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        realBlood2.rectTransform.pivot = new Vector2(0.5f, 0.5f);

        realBlood1.color = new Color(1, 1, 1, 1); // 알파 1로 고정
        realBlood1.rectTransform.localScale = Vector3.one * 1.1f;
        realBlood2.color = new Color(1, 1, 1, 1); // 알파 1로 고정
        realBlood2.rectTransform.localScale = Vector3.one * 1.15f;

        PlayBloodEffect(realBlood1, realBlood2);
        Managers.Sound.PlaySFX("InCorrect");
    }

    void PlayBloodEffect(Image realBlood1, Image realBlood2)
    {
        float fadeOutTime = 1.2f;
        float secondBloodDelay = 0.5f;
        float moveDistance = 100f; // 아래로 이동할 거리

        // 시작 위치 저장
        Vector2 startPos1 = realBlood1.rectTransform.anchoredPosition;
        Vector2 startPos2 = realBlood2.rectTransform.anchoredPosition;

        Sequence seq1 = DOTween.Sequence();
        seq1.AppendCallback(() => {
                bloodImage1.gameObject.SetActive(true);
                realBlood1.rectTransform.localScale = Vector3.one * 1.1f;
                realBlood1.color = new Color(1, 1, 1, 1);
                realBlood1.rectTransform.anchoredPosition = startPos1;
                realBlood1.gameObject.SetActive(true);
            })
            .AppendInterval(1f)
            .Append(
                realBlood1.DOFade(0f, fadeOutTime)
                    .SetEase(Ease.InOutSine)
                    .OnStart(() => {
                        realBlood1.rectTransform.DOAnchorPosY(startPos1.y - moveDistance, fadeOutTime).SetEase(Ease.InOutSine);
                    })
            )
            .AppendCallback(() => {
                realBlood1.color = new Color(1, 1, 1, 1);
                realBlood1.rectTransform.anchoredPosition = startPos1;
                realBlood1.gameObject.SetActive(false);
            });

        Sequence seq2 = DOTween.Sequence();
        seq2.AppendInterval(secondBloodDelay)
            .AppendCallback(() => {
                bloodImage2.gameObject.SetActive(true);
                realBlood2.rectTransform.localScale = Vector3.one * 1.15f;
                realBlood2.color = new Color(1, 1, 1, 1);
                realBlood2.rectTransform.anchoredPosition = startPos2;
                realBlood2.gameObject.SetActive(true);
            })
            .AppendInterval(1f)
            .Append(
                realBlood2.DOFade(0f, fadeOutTime)
                    .SetEase(Ease.InOutSine)
                    .OnStart(() => {
                        realBlood2.rectTransform.DOAnchorPosY(startPos2.y - moveDistance, fadeOutTime).SetEase(Ease.InOutSine);
                    })
            )
            .AppendCallback(() => {
                realBlood2.color = new Color(1, 1, 1, 1);
                realBlood2.rectTransform.anchoredPosition = startPos2;
                realBlood2.gameObject.SetActive(false);
            });
    }
}
