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
}

public class QuestionLogManager
{
    private readonly List<QuestionLog> _questionLogs = new List<QuestionLog>();

    public void AddQuestionLog(QuestionLog questionLog)
    {
        _questionLogs?.Add(questionLog);
    }

    public void ModifyQuestionLog(string answerer = null, string answer = null)
    {
        int lastIndex = _questionLogs.Count - 1;
            
        QuestionLog lastLog = _questionLogs[lastIndex];
        
        if(answerer != null)
            lastLog.answerer = answerer;
        if(answer != null)
            lastLog.answer = answer;
            
        _questionLogs[lastIndex] = lastLog;
    }

    public QuestionLog GetLastQuestionLog()
    {
        return _questionLogs[^1];
    }

    public int GetLogCount()
    {
        return _questionLogs.Count;
    }
}