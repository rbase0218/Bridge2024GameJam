using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WordCheckTimerLayer : MonoBehaviour, ILayoutControl, IUserData, ITimerControl
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

    private void ResetTimer()
    {
        currentTime = maxTime;
        gauge.fillAmount = 1;
    }
    
    public void ExitLayout()
    {
        CurtUser.myTurn = false;
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        Users = users;
        CurtUser = curUser;
        jobText.text = Utils.ChangeEnum(CurtUser.jobType);
        CurtUser.myTurn = true;
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
            case EJobType.VIP:
                spyPanel.gameObject.SetActive(false);
                cardPanel.SetActive(true);
                card.SetActive(true);
                descryptionText.text = "카드를 뒤집어서\n 제시어를 확인하세요.";
                break;
            case EJobType.Clown:
                spyPanel.gameObject.SetActive(false);
                cardPanel.SetActive(true);
                card.SetActive(true);
                descryptionText.text = "카드를 뒤집어서\n 제시어를 확인하세요.";
                break;
            case EJobType.Assassin:
                descryptionText = spyPanel.GetComponentInChildren<TMP_Text>();
                card.SetActive(false);
                cardPanel.SetActive(false);
                spyPanel.SetPanelLayout(Users, ESelectType.Hostage);
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
            case EJobType.VIP:
                card.SetActive(false);
                descryptionText.text = "카드를 뒤집어서\n 제시어를 확인하세요.";
                break;
            case EJobType.Clown:
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
        if(CurtUser.jobType == EJobType.Assassin)
            spyPanel.SetRandomSelectedUserIndex();
        
        //제한시간 종료 화면 보여줌
        cardPanel.SetActive(false);
        spyPanel.gameObject.SetActive(false);
        jobObject.SetActive(false);
        timeWaitText.SetActive(false);
        timeOverGruop.SetUserData(Users, CurtUser.index);
        timeOverGruop.gameObject.SetActive(true);
    }

    public void PauseTimer()
    {
        isPlaying = false;
    }

    public void RestartTimer()
    {
        isPlaying = true;
    }
}
