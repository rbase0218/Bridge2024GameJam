using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

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

    public UIThis uiThis;
    public UIWordCheck uiWordCheck;
    public UINoneBG uiNoneBg;
    public UIQuestion uiQuestion;
    public UIVote uiVote;
    public UIResult uiResult;

    private List<UserInfo> userList;

    private int curUserCount;
    private int roundCount;
    private string secretWord;
    private UserInfo hostageUser;
    private UserInfo questionUser;
    private UserInfo answerUser;
    public EPageType curPageType;
    public List<int> voteList;

    private void Start()
    {
        var categoryWord = Managers.Data.categoryArray[Managers.Game.currCategoryIndex];
        var len = Managers.Data.categoryDic[categoryWord].Length;
        secretWord = Managers.Data.categoryDic[categoryWord][Random.Range(0, len)];

        userList = Managers.Game._saveUserInfoList;
        roundCount = Managers.Game.gameRound;

        curUserCount = 0;
        roundCount = 0;
        curPageType = EPageType.WordCheck;
        voteList = new List<int>(6) {0,0,0,0,0,0};

        StartRound();
    }

    private void StartRound()
    {
        UIGauge.instance.SetActive(false);
        OffAllFrame();
        uiThis.gameObject.SetActive(true);
        uiThis.OpenFrame(8);
        uiThis.SetFrame8(userList[curUserCount].name);
        // 원래 인트로로 시작
    }

    public void InitUserQuestion()
    {
        userList.ForEach(x => { x.hasQuestion = false; });

        voteList.ForEach(x => { x = 0; });
    }

    public void SetCurHostage(int index)
    {
        if (hostageUser != null)
        {
            hostageUser.curHostage = false;
        }

        hostageUser = userList[index];
        hostageUser.curHostage = true;
        hostageUser.hasHostage = true;

        userList.ForEach(x =>
        {
            if (x.hasHostage)
                Debug.Log(userList[index].name + " is Hostage");
        });
    }

    public void SetAnswerTarget(string name)
    {
        answerUser = FindAnswerTarget(name);
    }

    public int GetNameToUserIndex(string name)
    {
        return FindAnswerTarget(name).index;
    }

    public UserInfo FindAnswerTarget(string name)
    {
        return userList.Find((x) => x.name == name);
    }

    public void OffAllFrame()
    {
        uiThis.gameObject.SetActive(false);
        uiWordCheck.gameObject.SetActive(false);
        uiNoneBg.gameObject.SetActive(false);
        uiQuestion.gameObject.SetActive(false);
        uiVote.gameObject.SetActive(false);
    }

    public void GoTimeWaitFrame()
    {
        OffAllFrame();
        uiNoneBg.gameObject.SetActive(true);
        uiNoneBg.OpenFrame(14);
    }

    #region WordCheck

    public void OpenFrameThis(int count)
    {
        uiThis.OpenFrame(count);

        if (count == 8)
        {
            uiThis.SetFrame8(userList[curUserCount].name);
        }
        else if (count == 9)
        {
            uiThis.SetFrame9(userList[curUserCount].name, userList[curUserCount].jobType);
        }
    }

    public void OpenWordCheck()
    {
        curPageType = EPageType.WordCheck;
        OffAllFrame();
        uiWordCheck.gameObject.SetActive(true);
        uiWordCheck.StartGauge();

        uiWordCheck.SetLayout(userList[curUserCount].jobType);
        if (userList[curUserCount].jobType == EJobType.Assassin)
        {
            uiWordCheck.SetData(userList.Select(x => x.name).ToArray());
        }
        else
        {
            uiWordCheck.SetCardData(secretWord);
            uiWordCheck.StartGauge();
        }
    }

    public void NextWordCheckUser()
    {
        curUserCount++;
        UIGauge.instance.SetActive(false);

        if (curUserCount >= userList.Count)
        {
            StartQuestionRound();
            return;
        }

        OffAllFrame();
        uiThis.gameObject.SetActive(true);

        uiThis.OpenFrame(8);
        uiThis.SetFrame8(userList[curUserCount].name);
    }

    #endregion

    #region Question

    private void StartQuestionRound()
    {
        curUserCount = 0;
        OffAllFrame();
        uiNoneBg.gameObject.SetActive(true);
        uiNoneBg.OpenFrame(20);
        curPageType = EPageType.Question;
    }

    public void PresentationRound()
    {
        curPageType = EPageType.Question;
        OffAllFrame();
        uiNoneBg.gameObject.SetActive(true);
        List<UserInfo> notHostageList = new List<UserInfo>(userList);
        notHostageList.RemoveAll(x => x.curHostage);
        notHostageList.RemoveAll(x => x.isDead);

        questionUser = notHostageList[Random.Range(0, notHostageList.Count)];
        questionUser.hasQuestion = true;

        uiNoneBg.SetFrame15(hostageUser.name, questionUser.name);
        uiNoneBg.OpenFrame(15);
    }

    public void GoQuestionRound()
    {
        List<UserInfo> notHostageList = new List<UserInfo>(userList);
        notHostageList.RemoveAll(x => x.isDead);
        notHostageList.RemoveAll(x => x.curHostage);
        notHostageList.RemoveAll(x => x.hasQuestion);

        if (notHostageList.Count == 0)
        {
            Debug.Log("Question Round End");
            GoDebatePage();
            return;
        }

        OffAllFrame();
        uiQuestion.gameObject.SetActive(true);
        uiQuestion.SetTitle(questionUser.name);
        uiQuestion.OpenFrame(21);
    }

    public void QuestionSelectPage()
    {
        List<UserInfo> notHostageList = new List<UserInfo>(userList);
        notHostageList.RemoveAll(x => x.curHostage);
        notHostageList.RemoveAll(x => x.isDead);
        notHostageList.RemoveAll(questionUser.Equals);

        uiQuestion.SetData(notHostageList.Select(x => x.name).ToArray());
        uiQuestion.OpenFrame(22);
    }

    public void GoAnswerPage()
    {
        OffAllFrame();

        uiWordCheck.SetSecondTitle(2);
        uiWordCheck.SetTitle(answerUser.name);
        uiQuestion.SetTitle(answerUser.name);
        uiWordCheck.ResetCard();
        uiWordCheck.gameObject.SetActive(true);
    }

    public void GoNextAnswerPage()
    {
        OffAllFrame();

        uiQuestion.SetFrame24(uiQuestion.GetQuestionText());
        uiQuestion.gameObject.SetActive(true);
        uiQuestion.OpenFrame(24);
    }

    // 예 아니오 눌렀을 때
    public void GoAnswerSelectedPage()
    {
        if (questionUser.hasQuestion)
        {
            List<UserInfo> notHostageList = new List<UserInfo>(userList);
            notHostageList.RemoveAll(x => x.isDead);
            notHostageList.RemoveAll(x => x.curHostage);
            notHostageList.RemoveAll(x => x.hasQuestion);

            if (notHostageList.Count == 0)
            {
                Debug.Log("Question Round End");

                OffAllFrame();

                uiNoneBg.SetFrame25(answerUser.name, "종료");
                uiNoneBg.OpenFrame(25);
                uiNoneBg.gameObject.SetActive(true);

                return;
            }

            questionUser = notHostageList[Random.Range(0, notHostageList.Count)];
            questionUser.hasQuestion = true;
        }

        OffAllFrame();

        uiNoneBg.SetFrame25(answerUser.name, questionUser.name);
        uiNoneBg.OpenFrame(25);
        uiNoneBg.gameObject.SetActive(true);
    }

    #endregion

    #region DebatePage

    public void GoDebatePage()
    {
        OffAllFrame();
        UIGauge.instance.SetActive(true);
        UIGauge.instance.SetTime(60f);
        UIGauge.instance.Play();
        uiNoneBg.gameObject.SetActive(true);
        uiNoneBg.OpenFrame(26);
    }

    public void GoSkipPage()
    {
        OffAllFrame();
        uiWordCheck.RemoveGauge();
        uiNoneBg.gameObject.SetActive(true);
        uiNoneBg.OpenFrame(27);
        curUserCount = 0;
    }

    #endregion


    #region VotePage

    public int globalIndex = 0;
    public void GoVotePage()
    {
        if(curUserCount >= userList.Count)
        {
            Debug.Log("Vote End");
            curUserCount = 0;
        
            var asdf = Mathf.CeilToInt(userList.Count / 2f);
            
            for (int i = 0; i < voteList.Count; ++i)
            {
                if (voteList[i] == 0)
                    continue;
            
                if (voteList[i] >= asdf)
                {
                    globalIndex = i;
                    uiNoneBg.OpenFrame(29);
                    return;
                }
            }
            
            GoQuestionRound();
            return;
        }
        
        OffAllFrame();
        UIGauge.instance.SetActive(true);
        UIGauge.instance.SetTime(10f);
        UIGauge.instance.Play();
        
        uiVote.Init();
        uiVote.SetData(userList[curUserCount].name, userList.Select( x => x.name).ToArray());
        uiVote.OpenFrame();
        
        curUserCount++;
    }

    
    // 투표 완료 버튼을 눌렀을 경우, 사용되는 메서드
    public void GoNextVoteTurnPage()
    {
        OffAllFrame();

        string name2 = "";
        
        if (curUserCount + 1 >= userList.Count)
        {
            name2 = "종료";
        }
        else
        {
            name2 = userList[curUserCount + 1].name;
        }
        

        if(name2 != "종료")
            uiNoneBg.SetFrame25(userList[curUserCount].name, name2);
        else
            uiNoneBg.SetFrame25(name2, "");
        uiNoneBg.OpenFrame(25);
        uiNoneBg.gameObject.SetActive(true);
    }

    #endregion
    
    #region UI-Result

    public void OnTypeAResultBoard()
    {
        uiResult.OpenType(false);
        uiResult.OpenFrameA_BG_Frame(userList[globalIndex].jobType);
        
        uiResult.SetFrame30(userList[globalIndex].name);
        uiResult.OpenFrameA(30);
    }

    public void OnFrame31Board()
    {
        uiResult.SetFrame31353637(userList[globalIndex].name, userList[globalIndex].jobType);
    }
    
    #endregion
}