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
    [SerializeField] private GameObject gruop;
    [SerializeField] private GameObject slider;
    
    [Header("Text")]
    [SerializeField] private TMP_Text descryptionText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text answerText;
    [SerializeField] private GameObject timeWaitText;

    [Header("Object")] 
    [SerializeField] private GameObject timer;

    [SerializeField]
    private GameObject selectButtons;
    [SerializeField] private GameObject card;
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
                if(timeOverGruop.gameObject.activeInHierarchy == false)
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

    public void ExitLayout()
    {
        card.SetActive(true);
        timer.SetActive(false);
        selectButtons.SetActive(false);
        timeOverGruop.gameObject.SetActive(false);
        gruop.SetActive(true);
        gameObject.SetActive(false);
        CurtUser.myTurn = false;
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        Users = users;
        CurtUser = curUser;
        descryptionText.text = "카드를 뒤집어\n 질문을 선택해주세요.";
        nameText.text = CurtUser.name;
        gameObject.SetActive(true);
    }
    
    public void ChangeDisplay()
    {
        //패널 끄기
        gruop.SetActive(false);
        timeWaitText.SetActive(true);
    }

    private void TimeOver()
    {
        gruop.SetActive(false);
        timeWaitText.SetActive(false);
        //제한시간 종료 화면 보여줌
        gruop.SetActive(false);
        
        try
        {
            var nextUser = TestManager.instance.GetNextQuestionUser();

            if(nextUser == null)
            {
                timeOverGruop.SetUserDataForNextQuestion(CurtUser, "종료");
            }
            else
            {
                timeOverGruop.SetUserDataForNextQuestion(CurtUser, nextUser.name);
            }
        }
        catch
        {
            
        }
        

        
        timeOverGruop.gameObject.SetActive(true);
    }

    public void SaveAnswer(string answer)
    {
        answerText.text = answer;
    }

    public void OnclickCardButton()
    {
        gauge.fillAmount = 1;
        currentTime = maxTime;
        timer.SetActive(true);
        isPlaying = true;
        descryptionText.text = "질문을 읽고\n 답을 선택해주세요.";
        card.SetActive(false);
        selectButtons.SetActive(true);
    }
}
