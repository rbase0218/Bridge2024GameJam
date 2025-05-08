using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicPicker
{
    private string _topic;
    public string Topic { get { return _topic; } }
    
    public void PickTopic(int index)
    {
        var maxNum = Managers.Data.wordArray[index].Length;
        var randNum = Random.Range(0, maxNum);

        _topic = Managers.Data.wordArray[index][randNum];
    }

    public bool IsTopicMatching(string text)
    {
        return _topic == text;
    }
}
 