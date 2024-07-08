using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    public static UIGauge instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public Image _gauge;
    public UnityEvent onEndGauge;
    public bool isPlaying = false;

    private float timer = .0f;
    private float maxTime = .0f;

    public void ResetGauge()
    {
        _gauge.fillAmount = 1f;
    }

    public void SetTime(float time = 1f)
    {
        maxTime = time;
    }

    public void Play()
    {
        StartCoroutine("StartGauge");
        isPlaying = true;
    }

    private IEnumerator StartGauge()
    {
        var startValue = _gauge.fillAmount;
        timer = .0f;

        while (timer < maxTime)
        {
            _gauge.fillAmount = Mathf.Lerp(startValue, 0f, timer / maxTime);
            yield return null;
            timer += Time.deltaTime;
        }

        onEndGauge?.Invoke();
        ResetGauge();
        isPlaying = false;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    
}