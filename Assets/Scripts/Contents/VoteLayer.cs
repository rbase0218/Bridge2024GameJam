using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoteLayer : MonoBehaviour, ILayoutControl, IUserData
{
    public List<UserInfo> Users { get; set; }
    public UserInfo CurtUser { get; set; }
    
    [Header("Gauge")]
    [SerializeField] private float maxTime;
    [SerializeField] private Image gauge;
    
    [Header("Text")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject timeWaitText;
    
    [Header("Panel")]
    [SerializeField] private SelectLayer votePanel;
    [SerializeField] private SelectLayer spyPanel;
    [SerializeField] private TimeOverGruop timeOverGruop;
    [SerializeField] private GameObject namePanel;

    [Header("Button")]
    [SerializeField] private Button[] buttons;
    
    private TMP_Text descryptionText;
    private float currentTime;
    private bool isPlaying;
    
    public void ExitLayout()
    {
        CurtUser.myTurn = false;
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        Users = users;
        CurtUser = curUser;
        CurtUser.myTurn = true;
        nameText.text = CurtUser.name;
        ChangeRole();
        gameObject.SetActive(true);
    }

    private void ChangeRole()
    {
        timeOverGruop.gameObject.SetActive(false);
        namePanel.SetActive(true);
        buttons.ToList().ForEach(x=> x.interactable=false);

        switch (CurtUser.jobType)
        {
            case EJobType.VIP:
            case EJobType.Clown:
                descryptionText = votePanel.GetComponentInChildren<TMP_Text>();
                spyPanel.gameObject.SetActive(false);
                votePanel.SetPanelLayout(Users, ESelectType.Vote);
                votePanel.gameObject.SetActive(true);
                descryptionText.text = "암살자라고 생각하는 사람을\n 선택해주세요.";
                break;
            case EJobType.Assassin:
                descryptionText = spyPanel.GetComponentInChildren<TMP_Text>();
                votePanel.gameObject.SetActive(false);
                spyPanel.SetPanelLayout(Users, ESelectType.Hostage);
                spyPanel.gameObject.SetActive(true);
                descryptionText.text = "인질로 잡을 사람을\n 선택하세요.";
                break;
            default:
                break;
        }
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
    
    public void ChangeDisplay()
    {
        //패널 끄기
        votePanel.gameObject.SetActive(false);
        spyPanel.gameObject.SetActive(false);
        namePanel.SetActive(false);
        timeWaitText.SetActive(true);
    }

    private void TimeOver()
    {
        if(CurtUser.jobType == EJobType.Assassin)
            spyPanel.SetRandomSelectedUserIndex();
        
        //제한시간 종료 화면 보여줌
        votePanel.gameObject.SetActive(false);
        spyPanel.gameObject.SetActive(false);
        namePanel.SetActive(false);
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
