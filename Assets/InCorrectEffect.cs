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

        realBlood1.color = new Color(1, 1, 1, 0);
        realBlood1.rectTransform.localScale = Vector3.zero;
        realBlood2.color = new Color(1, 1, 1, 0);
        realBlood2.rectTransform.localScale = Vector3.zero;

        PlayBloodEffect(realBlood1, realBlood2);
    }

    void PlayBloodEffect(Image realBlood1, Image realBlood2)
    {
        Sequence seq1 = DOTween.Sequence();
        seq1.AppendCallback(() => {
                bloodImage1.gameObject.SetActive(true); // 애니메이션 시작 직전 마스크 활성화
                realBlood1.rectTransform.localScale = Vector3.zero;
                realBlood1.color = new Color(1, 1, 1, 0);
                realBlood1.gameObject.SetActive(true);
            })
            .Append(realBlood1.rectTransform.DOScale(1.1f, 0.18f).SetEase(Ease.OutBack))
            .Join(realBlood1.DOFade(1f, 0.12f))
            .Append(realBlood1.rectTransform.DOScale(1f, 0.08f).SetEase(Ease.InOutSine))
            .AppendInterval(0.25f)
            .Append(realBlood1.DOFade(0f, 0.7f))
            .AppendCallback(() => {
                realBlood1.rectTransform.localScale = Vector3.zero;
                realBlood1.gameObject.SetActive(false);
            });

        Sequence seq2 = DOTween.Sequence();
        seq2.AppendInterval(0.28f) // 첫번째 피보다 약간 늦게 시작
            .AppendCallback(() => {
                bloodImage2.gameObject.SetActive(true); // 두번째 피 시작 직전 마스크 활성화
                realBlood2.rectTransform.localScale = Vector3.zero;
                realBlood2.color = new Color(1, 1, 1, 0);
                realBlood2.gameObject.SetActive(true);
            })
            .Append(realBlood2.rectTransform.DOScale(1.15f, 0.19f).SetEase(Ease.OutBack))
            .Join(realBlood2.DOFade(1f, 0.13f))
            .Append(realBlood2.rectTransform.DOScale(1f, 0.09f).SetEase(Ease.InOutSine))
            .AppendInterval(0.3f)
            .Append(realBlood2.DOFade(0f, 0.8f))
            .AppendCallback(() => {
                realBlood2.rectTransform.localScale = Vector3.zero;
                realBlood2.gameObject.SetActive(false);
            });
    }
}
