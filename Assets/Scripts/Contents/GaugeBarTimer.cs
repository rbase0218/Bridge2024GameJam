using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBarTimer : MonoBehaviour
{
    public static Action OnTimerEnd;
    
    [SerializeField] 
    private Image gauge;
    
    [SerializeField]
    private float maxTime;
    private float currentTime;
    private bool isPlaying;

    private void Start()
    {
        isPlaying = false;
        StartTimer();
    }

    private void OnDisable()
    {
        StopTimer();
        ResetTimer();
    }

    private void Update()
    {
        if(currentTime > 0 && isPlaying)
        {
            currentTime -= Time.deltaTime;
            gauge.fillAmount = currentTime / maxTime;

            if(currentTime <= 0)
            {
                isPlaying = false;
                OnTimerEnd?.Invoke();
            }
        }
    }
    
    public void StartTimer()
    {
        ResetTimer();
        isPlaying = true;
    }
    
    public void StopTimer()
    {
        isPlaying = false;
    }

    private void ResetTimer()
    {
        currentTime = maxTime;
        gauge.fillAmount = 1;
    }
    
    public void PlusTime(float time)
    {
        currentTime += time;
        
        if (currentTime > maxTime)
        {
            currentTime = maxTime;
        }
        
        gauge.fillAmount = currentTime / maxTime;
    }
}
