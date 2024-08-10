using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Gauge : UIBase
{
    public UnityEvent onStartGauge;
    public UnityEvent onEndGauge;
    
    public UnityAction<float> onGaugeTimer;

    private Image _gaugeFillImage;
    
    [field: SerializeField]
    public float GaugeTime { get; private set; }
    
    private float _timer;
    private bool _isPlay;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        _gaugeFillImage = Utils.FindChild<Image>(gameObject, "GaugeFill", true);
        
        return true;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            Play();
    }

    public void Play()
    {
        if (_isPlay)
        {
            Debug.Log("이미 Gauge가 실행중 입니다.");
            return;
        }
        
        _isPlay = true;
        StartCoroutine("StartGauge");
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
        
        onEndGauge.Invoke();
        _timer = .0f;
        _isPlay = false;
    }
}
