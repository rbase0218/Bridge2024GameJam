using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestManager : MonoBehaviour
{
    public static TestManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    [SerializeField] private GameObject[] layouts;
    [SerializeField] private TMP_Text wordText;
    private UserInfo curQuestionUser;
    private UserInfo prevQuestionUser;

    private List<UserInfo> userInfos;
    private GameObject currentLayout;
    private int currentLayoutIndex;
    private int userCount;
    private UserInfo hostageTargetUser;
    private UserInfo questionTargetUser;
    private string currentWord;

    private bool isQuestionIntroEnd;
    public bool isQuestionEnd;
    public EVoteType voteType;
    public UserInfo voteTargetUser;

    private List<int> voteUsers = new List<int>();

    private int roundCount;
    private bool isCorrect;

    private void Start()
    {
        SelectPanel.OnSelectHostage += SetHostage;
        SelectPanel.OnSelectQuestion += SetQuestionTarget;
        SelectPanel.OnSelectVote += SetVote;
        NextOrderLayer.OnExitLayout += GoJobOpenLayout;
        NextOrderVoteLayer.OnExitLayout += GoVoteLayout;
        VoteIntroLayer.OnExitLayout += GoNextOrderVoteLayout;
        userInfos = Managers.Game._saveUserInfoList;

        List<EJobType> jobTypes = new List<EJobType>();

        switch (userInfos.Count)
        {
            case 3:
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.Assassin);
                break;
            case 4:
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.Assassin);
                break;
            case 5:
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.Clown);
                jobTypes.Add(EJobType.Assassin);
                break;
            case 6:
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.VIP);
                jobTypes.Add(EJobType.Clown);
                jobTypes.Add(EJobType.Assassin);
                break;
        }

        int random1,  random2;
        EJobType temp;

        for (int i = 0; i < jobTypes.Count; ++i)
        {
            random1 = Random.Range(0, jobTypes.Count);
            random2 = Random.Range(0, jobTypes.Count);

            temp = jobTypes[random1];
            jobTypes[random1] = jobTypes[random2];
            jobTypes[random2] = temp;
        }
        
        for (int i = 0; i < userInfos.Count; i++)
        {
            userInfos[i].SetJob(jobTypes[i]);
            Debug.Log(jobTypes[i]);
        }

        var categoryWord = Managers.Data.categoryArray[Managers.Game.currCategoryIndex];
        var len = Managers.Data.categoryDic[categoryWord].Length;
        currentWord = Managers.Data.categoryDic[categoryWord][Random.Range(0, len)];

        for (int i = 0; i < userInfos.Count; i++)
        {
            voteUsers.Add(0);
        }

        wordText.text = currentWord;
        StartLayout();
    }

    private void OnDestroy()
    {
        SelectPanel.OnSelectHostage -= SetHostage;
        SelectPanel.OnSelectQuestion -= SetQuestionTarget;
        SelectPanel.OnSelectVote -= SetVote;
        NextOrderLayer.OnExitLayout -= GoJobOpenLayout;
        NextOrderVoteLayer.OnExitLayout -= GoVoteLayout;
        VoteIntroLayer.OnExitLayout -= GoNextOrderVoteLayout;
    }

    private void SetQuestionTarget(int index)
    {
        questionTargetUser = userInfos[index];
    }

    private void SetVote(int index)
    {
        voteUsers[index] += 1;
    }

    public void GetVoteResult()
    {
        int max = 0;
        int maxIndex = 0;

        for (int i = 0; i < voteUsers.Count; i++)
        {
            if (voteUsers[i] > max)
            {
                max = voteUsers[i];
                maxIndex = i;
            }
        }

        for (int i = 0; i < voteUsers.Count; i++)
        {
            if (voteUsers[i] == max && i != maxIndex)
            {
                Debug.Log("동점자 발생");
                // 토론부터 다시 시작

                voteUsers.ForEach(x => x = 0);
                voteType = EVoteType.Same;

                return;
            }
        }

        voteTargetUser = userInfos[maxIndex];

        switch (userInfos[maxIndex].jobType)
        {
            case EJobType.VIP:
                // 시민 결과창
                userInfos[maxIndex].isDead = true;
                // 라운드 인원 수 넘었는지 확인 넘었으면 종료
                Debug.Log("귀빈 사망");

                if (roundCount >= userInfos.Count - 1)
                {
                    Debug.Log("스파이 승리 화면");

                    return;
                }

                roundCount++;

                voteUsers.ForEach(x => x = 0);

                voteType = EVoteType.VIP;

                // 라운드 다시 시작
                // GoQuestionIntroLayout();
                // // 투표 초기화
                break;
            case EJobType.Clown:
                // 광대 
                voteType = EVoteType.Clown;
                Debug.Log("광대 선택");

                // 암살자 최종 기회
                break;
            case EJobType.Assassin:
                // 암살자 최종 기회
                voteType = EVoteType.Assassin;
                Debug.Log("암살자 선택");

                break;
        }
    }

    private void SetHostage(int index)
    {
        if (hostageTargetUser != null)
        {
            hostageTargetUser.curHostage = false;
        }

        hostageTargetUser = userInfos[index];

        if (hostageTargetUser.hasHostage)
        {
            Debug.Log("이미 선택된 유저입니다.");
            return;
        }

        hostageTargetUser.hasHostage = true;
        hostageTargetUser.curHostage = true;

        userInfos.ForEach(x =>
        {
            if (x.hasHostage)
            {
                Debug.Log(x.name + " 인질.");
            }
        });
    }

    public void StartLayout()
    {
        userCount = 0;
        roundCount++;
        currentLayoutIndex = 0;
        //currentLayoutIndex = (int)ELayoutName.VoteIntro;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
        userCount++;
    }

    public void RepeatLayout()
    {
        if (userCount >= userInfos.Count)
        {
            userCount = 0;
            NextLayout();
        }

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
        userCount++;
    }

    public void GoJobOpenLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.JobOpen;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void GoWordCheckLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.WordCheck;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void GoVoteLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.Vote;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
        userCount++;
    }

    public void InitUserCount()
    {
        userCount = 0;
    }

    public void GoDebateLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.Debate;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void GoQuestionIntroLayout()
    {
        GetNoVoteUsers();
        isQuestionIntroEnd = true;

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.QuestionIntro;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, curQuestionUser);
    }

    private void GetNoVoteUsers()
    {
        List<UserInfo> noVoteUsers = new List<UserInfo>(userInfos);
        noVoteUsers.RemoveAll(x => x.isDead);
        noVoteUsers.RemoveAll(x => x.curHostage);
        noVoteUsers.RemoveAll(x => x.hasQuestion);

        if (noVoteUsers.Count == 0)
        {
            isQuestionEnd = true;
            return;
        }

        curQuestionUser = noVoteUsers[Random.Range(0, noVoteUsers.Count)];
        curQuestionUser.hasQuestion = true;
    }

    public UserInfo GetNextQuestionUser()
    {
        isQuestionIntroEnd = true;
        GetNoVoteUsers();
        return curQuestionUser;
    }

    public void GoQuestionLayout()
    {
        if (isQuestionIntroEnd)
        {
            isQuestionIntroEnd = false;
        }
        else
        {
            GetNoVoteUsers();
        }

        if (isQuestionEnd)
        {
            Debug.Log("모든 유저가 투표를 완료했습니다.");
            NextLayout();
            return;
        }

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.Question;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, curQuestionUser);
    }

    public void GoNextOrderLayout()
    {
        userCount++;
        Debug.Log(userCount);

        if (userCount >= userInfos.Count)
        {
            GoQuestionIntroLayout();
            return;
        }

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.NextOrder;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void GoNextOrderVoteLayout()
    {
        Debug.Log(userCount);

        if (userCount >= userInfos.Count)
        {
            Debug.Log("투표 결과 확인");
            currentLayoutIndex = (int)ELayoutName.Vote;
            GetVoteResult();
            NextLayout();
            return;
        }

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.NextOrderVote;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void GoAnswerLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.Answer;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, questionTargetUser);
    }

    public void GoFinalLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.Final;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void GoEndLayout()
    {
        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayoutIndex = (int)ELayoutName.End;
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void NextLayout()
    {
        userCount = 0;
        currentLayoutIndex++;

        if (currentLayoutIndex >= layouts.Length)
        {
            // 게임 종료
            Debug.Log("게임 종료");
            return;
        }

        currentLayout.GetComponent<ILayoutControl>()?.ExitLayout();
        currentLayout = layouts[currentLayoutIndex];
        currentLayout.GetComponent<ILayoutControl>()?.StartLayout(userInfos, userInfos[userCount]);
    }

    public void AnswerCheck(string answer)
    {
        if (answer == currentWord)
        {
            Debug.Log("정답");
            isCorrect = true;
            GoEndLayout();
        }
        else
        {
            Debug.Log("오답");
            isCorrect = false;
            GoEndLayout();
        }
    }
}