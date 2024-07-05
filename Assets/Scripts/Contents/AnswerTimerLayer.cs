using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AnswerTimerLayer : MonoBehaviour, ILayoutControl, IUserData
{
    [Header("Gauge")]
    [SerializeField] private float maxTime;
    [SerializeField] private Image gauge;
    
    [Header("Text")]
    [SerializeField] private TMP_Text descryptionText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text answerText;

    [Header("Object")]
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject cardPanel;
    [SerializeField] private GameObject nameObject;
    [SerializeField] private TimeOverGruop timeOverGruop;
    
    [Header("Button")]
    [SerializeField] private Button[] buttons;

    private float currentTime;
    private bool isPlaying;
    
    public List<UserInfo> Users { get; set; }
    public UserInfo CurtUser { get; set; }

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

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        nameText.text = CurtUser.name;
        gameObject.SetActive(true);
    }

    private void TimeOver()
    {
        //제한시간 종료 화면 보여줌
        cardPanel.SetActive(false);
        nameObject.SetActive(false);
        timeOverGruop.SetUserData(Users, CurtUser.index);
        timeOverGruop.gameObject.SetActive(true);
    }

    public void SaveAnswer(string answer)
    {
        answerText.text = answer;
    }
}
