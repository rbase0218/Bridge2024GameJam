using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalLayer : MonoBehaviour, ILayoutControl
{
    public static Action OnExitLayout;
    
    [Header("Gauge")]
    [SerializeField] private float maxTime;
    [SerializeField] private Image gauge;

    
    
    [Header("Text")]
    [SerializeField] private TMP_Text jobText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text descryptionText;
    
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject resultObject;
    private float currentTime;
    private bool isPlaying;
    private bool isClick;
    private bool isAssasinWin;

    private string answer;

    private void Start()
    {
        inputField.onValueChanged.AddListener(InputText);
    }
    
    private void InputText(string text)
    {
        answer = text;
    }

    public void OnClickAnswerCheck()
    {
        isClick = true;
        AnswerResult();
    }

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
                if(isClick == false)
                {
                    AnswerResult();
                }
            }
        }
    }

    private void AnswerResult()
    {
        isAssasinWin = TestManager.instance.AnswerCheck(answer);
        resultObject.SetActive(true);
        if(isAssasinWin)
        {
            resultText.text = "정답입니다!";
            jobText.text = "암살자";
            descryptionText.text = "암살자가\n 귀빈들과의 대결에서\n 승리했습니다.";
        }
        else
        {
            resultText.text = "오답입니다!";
            if (TestManager.instance.voteTargetUser.jobType == EJobType.VIP)
            {
                jobText.text = "귀빈";
                descryptionText.text = "귀빈들이 그들의\n 무도회를\n 지켜냈습니다.";
            }
            else
            {
                jobText.text = "광대";
                descryptionText.text = "뜻밖의 광대가\n 귀빈들과의 게임에서\n 승리를 가져갑니다.";
            }
        }
    }
    
    public void OnClickExit()
    {
        // 최종 결과 창으로
    }

    private void OnEnable()
    {
        isPlaying = false;
        StartTimer();
    }
}
