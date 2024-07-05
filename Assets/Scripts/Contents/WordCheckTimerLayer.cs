using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WordCheckTimerLayer : MonoBehaviour, ILayoutControl, IUserData
{
    public List<UserInfo> Users { get; set; }
    public UserInfo CurtUser { get; set; }
    
    [Header("Gauge")]
    [SerializeField] private float maxTime;
    [SerializeField] private Image gauge;
    
    [Header("Text")]
    [SerializeField] private TMP_Text wordText;
    [SerializeField] private GameObject timeWaitText;
    
    [Header("Panel")]
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject cardPanel;
    [SerializeField] private SelectLayer spyPanel;
    [SerializeField] private TimeOverGruop timeOverGruop;
    
    [Header("Job")]
    [SerializeField] private GameObject jobObject;
    [SerializeField] private TMP_Text jobText;
    
    [Header("Button")]
    [SerializeField] private Button[] buttons;
    
    private TMP_Text descryptionText;
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

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        Users = users;
        CurtUser = curUser;
        jobText.text = Utils.ChangeEnum(CurtUser.jobType);
        gameObject.SetActive(true);
        ChangeRole();
    }

    private void ChangeRole()
    {
        timeOverGruop.gameObject.SetActive(false);
        jobObject.SetActive(true);
        buttons.ToList().ForEach(x=> x.interactable=false);

        descryptionText = cardPanel.GetComponentInChildren<TMP_Text>();
        switch (CurtUser.jobType)
        {
            case EJobType.Citizen:
                spyPanel.gameObject.SetActive(false);
                cardPanel.SetActive(true);
                card.SetActive(true);
                descryptionText.text = "카드를 뒤집어서\n 제시어를 확인하세요.";
                break;
            case EJobType.Actor:
                spyPanel.gameObject.SetActive(false);
                cardPanel.SetActive(true);
                card.SetActive(true);
                descryptionText.text = "카드를 뒤집어서\n 제시어를 확인하세요.";
                break;
            case EJobType.Spy:
                descryptionText = spyPanel.GetComponentInChildren<TMP_Text>();
                card.SetActive(false);
                cardPanel.SetActive(false);
                spyPanel.SetPanel(Users);
                spyPanel.gameObject.SetActive(true);
                descryptionText.text = "인질로 잡을 사람을\n 선택하세요.";
                break;
            default:
                break;
        }
    }

    public void OnClickCardButton()
    {
        buttons.ToList().ForEach(x=> x.interactable=true);
        switch (CurtUser.jobType)
        {
            case EJobType.Citizen:
                card.SetActive(false);
                descryptionText.text = "카드를 뒤집어서\n 제시어를 확인하세요.";
                break;
            case EJobType.Actor:
                card.SetActive(false);
                descryptionText.text = "배우 역할은 숨기고\n 스파이로 보일 수 있게끔\n 적절히 연기하세요.";
                break;
            default:
                break;
        }
    }
    
    public void ChangeDisplay()
    {
        //패널 끄기
        cardPanel.SetActive(false);
        spyPanel.gameObject.SetActive(false);
        jobObject.SetActive(false);
        timeWaitText.SetActive(true);
    }

    private void TimeOver()
    {
        //제한시간 종료 화면 보여줌
        cardPanel.SetActive(false);
        spyPanel.gameObject.SetActive(false);
        jobObject.SetActive(false);
        timeWaitText.SetActive(false);
        timeOverGruop.SetUserData(Users, CurtUser.index);
        timeOverGruop.gameObject.SetActive(true);
    }
}
