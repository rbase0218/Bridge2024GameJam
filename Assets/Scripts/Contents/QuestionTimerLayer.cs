using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuestionTimerLayer : MonoBehaviour, ILayoutControl
{
    [Header("Gauge")]
    [SerializeField] private float maxTime;
    [SerializeField] private Image gauge;
    
    [Header("Text")]
    [SerializeField] private TMP_Text descryptionText;
    [SerializeField] private TMP_Text nameText;

    [Header("Object")]
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject cardPanel;
    [SerializeField] private GameObject nameObject;
    [SerializeField] private TimeOverGruop timeOverGruop;
    
    [Header("Button")]
    [SerializeField] private Button[] buttons;
    
    private List<UserInfo> userInfos;
    private UserInfo currentUser;

    private float currentTime;
    private bool isPlaying;

    private void Start()
    {
        isPlaying = false;
        StartTimer();
    }

    private void OnEnable()
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
                TimeOver();
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

    public void ExitLayout()
    {
        gameObject.SetActive(false);
    }

    public void StartLayout()
    {
        gameObject.SetActive(true);
    }

    public void SetUserData(List<UserInfo> users, int userCount)
    {
        userInfos = users;
        currentUser = userInfos[userCount];
        nameText.text = currentUser.name;
    }

    public void OnClickCardButton()
    {
        //
    }

    private void TimeOver()
    {
        //제한시간 종료 화면 보여줌
        cardPanel.SetActive(false);
        nameObject.SetActive(false);
        timeOverGruop.SetUserData(userInfos, currentUser.index);
        timeOverGruop.gameObject.SetActive(true);
    }
}
