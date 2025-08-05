using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestionLog
{
    // 질문자
    public string questioner;
    // 답변자
    public string answerer;

    // 질문 내용
    public string question;
    // 답변 내용
    public string answer;

    public QuestionLog(string questioner = null, string answerer = null, string question = null, string answer = null)
    {
        this.questioner = questioner;
        this.answerer = answerer;
        this.question = question;
        this.answer = answer;
    }

    public void SetAnswer(string answerer, string answer)
    {
        this.answerer = answerer;
        this.answer = answer;
    }
}

public class QuestionLogManager
{
    private readonly List<QuestionLog> _questionLogs = new List<QuestionLog>();
    private List<QuestionLog> _selectQuestion = new List<QuestionLog>();
    private int count = 0;
    private int _currentIndex = 0;

    public void AddQuestionLog(QuestionLog questionLog)
    {
        _questionLogs?.Add(questionLog);
    }

    public void ClearQuestionLog()
    {
        _questionLogs?.Clear();
    }

    public void SelectRandQuestion(int num)
    {
        count = num;
        _currentIndex = 0;

        HashSet<int> selectIdx = new HashSet<int>();

        while (selectIdx.Count < count && selectIdx.Count < _questionLogs.Count)
        {
            int randomNumber = Random.Range(0, _questionLogs.Count);
            selectIdx.Add(randomNumber);
        }
        _selectQuestion.Clear();
        
        foreach (int index in selectIdx)
            _selectQuestion.Add(_questionLogs[index]);
    }

    public QuestionLog GetRandomQuestionLog()
    {
        if (_currentIndex >= count)
            return new QuestionLog("NULL");
        return _selectQuestion[_currentIndex];
    }

    public bool NextQuestion()
    {
        _currentIndex++;

        if (_currentIndex >= count)
            return false;
        return true;
    }

    public int GetLogCount()
    {
        return _questionLogs.Count;
    }
}