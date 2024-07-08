using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteIntroLayer : MonoBehaviour, ILayoutControl
{
    public static Action OnExitLayout;
    
    [Header("Gauge")]
    [SerializeField] private float maxTime;
    [SerializeField] private Image gauge;

    private float currentTime;
    private bool isPlaying;
    private bool isClick;
    
    public void ExitLayout()
    {
        isPlaying = false;
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        isPlaying = true;
        gameObject.SetActive(true);
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
    
    private void Update()
    {
        if(currentTime > 0 && isPlaying)
        {
            currentTime -= Time.deltaTime;
            gauge.fillAmount = currentTime / maxTime;

            if(currentTime <= 0)
            {
                isPlaying = false;
                OnExitLayout?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        isPlaying = false;
        StartTimer();
    }
}
