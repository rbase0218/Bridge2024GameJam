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

    public void SetAnswer(string answer)
    {
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

    public void SaveSelectedNumber(int count)
    {
        this.count = count;
        PickRandomQuestion();
    }
    private void PickRandomQuestion()
    {
        _currentIndex = 0;

        HashSet<int> selectIdx = new HashSet<int>();

        while (selectIdx.Count < count && selectIdx.Count < _questionLogs.Count)
        {
            int randomNumber = Random.Range(0, _questionLogs.Count);
            selectIdx.Add(randomNumber);
        }
        
        // 선택된 퀘스트 목록을 지운다.
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

    public void SetCurrentQuestionAnswer(string answer)
    {
        if (_currentIndex >= count || _currentIndex < 0) return;
        
        // _selectQuestion에서 현재 질문의 답변 업데이트
        var currentQuestion = _selectQuestion[_currentIndex];
        currentQuestion.SetAnswer(answer);
        _selectQuestion[_currentIndex] = currentQuestion;
        
        // _questionLogs에서도 해당 질문을 찾아서 답변 업데이트
        for (int i = 0; i < _questionLogs.Count; i++)
        {
            if (_questionLogs[i].question == currentQuestion.question)
            {
                var questionLog = _questionLogs[i];
                questionLog.SetAnswer(answer);
                _questionLogs[i] = questionLog;
                break;
            }
        }
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

    public List<QuestionLog> GetAllQuestionLogs()
    {
        return new List<QuestionLog>(_questionLogs);
    }
}