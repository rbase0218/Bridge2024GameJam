using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Gauge : UIBase
{
    #region # [ Unity Events & Actions ] #
    
    public UnityEvent onStartGauge;
    public UnityEvent onEndGauge;
    
    public UnityAction<float> onGaugeTimer;
    
    #endregion

    #region # [ Components ] #
    
    private Image _gaugeFillImage;
    private RectTransform _rectTransform;
    
    #endregion
    
    #region # [ Properties ] #
    
    [field: SerializeField]
    public float GaugeTime { get; private set; }
    
    private float _timer;
    private bool _isPlay;
    
    // Hide
    private Vector2 _originSize;
    private Vector2 _hideSize;
    
    #endregion

    public bool isDebugMode = false;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        _gaugeFillImage = Utils.FindChild<Image>(gameObject, "GaugeFill", true);
        _rectTransform = Utils.TryOrAddComponent<RectTransform>(gameObject);

        _originSize = _rectTransform.sizeDelta;
        _hideSize = Vector2.zero;
        
        return true;
    }

    public bool Play()
    {
        if (_isPlay)
        {
            Debug.Log("이미 Gauge가 실행중 입니다.");
            return false;
        }
        
        if (_rectTransform.sizeDelta.x <= 0)
            _rectTransform.sizeDelta = _originSize;

        _isPlay = true;
        StartCoroutine("StartGauge");
        return true;
    }

    public void SetGauge(float time = 3f)
    {
        if (isDebugMode)
            return;
        
        GaugeTime = time;
        
        //TODO: 게이지 끝났을 때 GaugeTime 초기화 필요.
    }

    public void PlayHide()
    {
        var isPlay = Play();
        if (isPlay)
            _rectTransform.sizeDelta = _hideSize;
    }

    public void Stop()
    {
        StopCoroutine("StartGauge");
        
        _isPlay = false;
        _timer = .0f;
        SetGauge();
        _gaugeFillImage.fillAmount = 1f;
        onStartGauge.RemoveAllListeners();
        onEndGauge.RemoveAllListeners();
    }
    
    private IEnumerator StartGauge()
    {
        onStartGauge.Invoke();
        
        while (_timer <= GaugeTime)
        {
            _timer += Time.deltaTime;
            
            yield return null;

            _gaugeFillImage.fillAmount = 1f - _timer / GaugeTime;
            
            // Ratio를 전달한다.
            onGaugeTimer?.Invoke(_timer / GaugeTime);
        }
        
        _gaugeFillImage.fillAmount = 1f;
        _timer = .0f;
        _isPlay = false;
        
        SetGauge();
        onEndGauge.Invoke();
    }
}